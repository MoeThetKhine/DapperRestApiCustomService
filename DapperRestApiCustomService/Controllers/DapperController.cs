﻿namespace DapperRestApiCustomService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DapperController : ControllerBase
{
	private readonly DapperService _dapperService;

	public DapperController(DapperService dapperService)
	{
		_dapperService = dapperService;
	}

	#region GetAllBlogs

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

	#endregion

	#region CreateBlog

	[HttpPost]
	public async Task<IActionResult> CreateBlog([FromBody] BlogModel blog)
	{
		try
		{

			#region Validation

			if (blog is null)
			{
				return BadRequest("Please fill all field.");
			}
			if (string.IsNullOrWhiteSpace(blog.BlogTitle))
			{
				return BadRequest("Invalid Blog Data.");
			}
			if (string.IsNullOrWhiteSpace(blog.BlogAuthor))
			{
				return BadRequest("Invalid Blog Data");
			}
			if (string.IsNullOrWhiteSpace(blog.BlogContent))
			{
				return BadRequest("Invalid Blog Data");
			}

			#endregion

			var parameters = new List<SqlParameter>
		{
			new("@BlogTitle",blog.BlogTitle),
			new("@BlogAuthor",blog.BlogAuthor),
			new("@BlogContent",blog.BlogContent),
			new("@DeleteFlag", false),
		};


			int result = await _dapperService.ExecuteAsync(Query.CreateBlogQuery, parameters);
			return result > 0 ? Ok("Blog created successfully.") : StatusCode(500, "Failed to create blog.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	#endregion

	#region Update Blog

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogModel blog)
	{
		try
		{
			if (blog == null || string.IsNullOrWhiteSpace(blog.BlogTitle) || string.IsNullOrWhiteSpace(blog.BlogAuthor))
			{
				return BadRequest("Invalid blog data.");
			}

			var parameters = new
			{
				BlogId = id,
				BlogTitle = blog.BlogTitle,
				BlogAuthor = blog.BlogAuthor,
				BlogContent = blog.BlogContent ?? ""
			};

			int result = await _dapperService.ExecuteAsync(Query.UpdateBlogQuery, parameters);
			return result > 0 ? Ok("Blog updated successfully.") : NotFound("Blog not found.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	#endregion

	#region DeleteBlog

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBlog(int id)
	{
		try
		{
			var parameters = new { BlogId = id };
			int result = await _dapperService.ExecuteAsync(Query.DeleteBlogQuery, parameters);
			return result > 0 ? Ok("Blog marked as deleted successfully.") : NotFound("Blog not found.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	#endregion

}
