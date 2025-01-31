using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperRestApiCustomService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DapperController : ControllerBase
	{
		private readonly DapperService _dapperService;

		public DapperController(DapperService dapperService)
		{
			_dapperService = dapperService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBlogs()
		{
			try
			{
				var blogs = await _dapperService.QueryAsync<BlogModel>(Query.GetAllBlogsQuery);
				return Ok(blogs);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
