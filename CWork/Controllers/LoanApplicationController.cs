

using CWork.CQRS_Features.Loan.Command;
using CWork.CQRS_Features.Loan.Query;
using CWork.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/loan-applications")]
public class LoanApplicationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LoanApplicationController> _logger;

    public LoanApplicationController(IMediator mediator, ILogger<LoanApplicationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    private void LogAction(string actionName, IActionResult result)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var logMessage = $"Action: {actionName}, UserId: {userId}, Result: {result}";
        _logger.LogInformation(logMessage);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LoanApplicationDto>))]
    public async Task<IActionResult> GetLoanApplications()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = new GetLoanApplicationsQuery { UserId = userId };

        try
        {
            var loanApplications = await _mediator.Send(query);
            LogAction(nameof(GetLoanApplications), Ok(loanApplications));
            return Ok(loanApplications);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetLoanApplications request.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoanApplicationDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLoanApplicationById(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = new GetLoanApplicationByIdQuery { Id = id, UserId = userId };

        try
        {
            var loanApplication = await _mediator.Send(query);
            IActionResult result;

            if (loanApplication != null)
            {
                result = Ok(loanApplication);
            }
            else
            {
                result = NotFound("Loan application not found");
            }

            LogAction(nameof(GetLoanApplicationById), result); // Use LogAction method without returning

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetLoanApplicationById request.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateLoanApplicationCommand))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLoanApplication([FromBody] CreateLoanApplicationCommand command)
    {
        if (command == null)
        {
            return BadRequest("Loan application data is invalid.");
        }

        command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var id = await _mediator.Send(command);
            LogAction(nameof(CreateLoanApplication), CreatedAtAction(nameof(GetLoanApplications), new { id }, command));
            return CreatedAtAction(nameof(GetLoanApplications), new { id }, command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateLoanApplication request.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLoanApplication(int id, [FromBody] UpdateLoanApplicationCommand command)
    {
        if (command == null)
        {
            return BadRequest("Loan application data is invalid.");
        }
        var exist = await GetLoanApplicationById(id);
        if (exist.GetType() != typeof(LoanApplicationDto))
        {
            LogAction(nameof(UpdateLoanApplication), exist);
            return exist;
        }
        command.Id = id;
        command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            await _mediator.Send(command);
            LogAction(nameof(UpdateLoanApplication), Ok("Loan application updated successfully."));
            return Ok("Loan application updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateLoanApplication request.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLoanApplication(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var command = new DeleteLoanApplicationCommand { Id = id, UserId = userId };

        try
        {
            await _mediator.Send(command);
            LogAction(nameof(DeleteLoanApplication), NoContent());
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteLoanApplication request.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}
