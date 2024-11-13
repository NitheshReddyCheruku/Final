using GrowthPath.FeedbackAPI.Models;
using GrowthPath.FeedbackAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GrowthPath.FeedbackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks() =>
            Ok(await _feedbackRepository.GetAllFeedbacks());

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackById(id);
            return feedback == null ? NotFound() : Ok(feedback);
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbackByCourseId(int courseId) =>
            Ok(await _feedbackRepository.GetFeedbacksByCourseId(courseId));

        [HttpPost]
        public async Task<ActionResult> AddFeedback([FromBody] Feedback feedback)
        {
            await _feedbackRepository.AddFeedback(feedback);
            return CreatedAtAction(nameof(GetFeedback), new { id = feedback.FeedbackId }, feedback);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeedback(int id, [FromBody] Feedback feedback)
        {
            if (id != feedback.FeedbackId) return BadRequest();
            await _feedbackRepository.UpdateFeedback(feedback);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedback(int id)
        {
            await _feedbackRepository.DeleteFeedback(id);
            return NoContent();
        }
    }
}
