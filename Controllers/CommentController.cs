using Finshark_API.DTOs.Comment;
using Finshark_API.Helpers;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Finshark_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentInterface _commentInterface;
        private readonly IStockInterface _stockInterface;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fMPService;

        public CommentController(ICommentInterface commentInterface, IStockInterface stockInterface,
            UserManager<AppUser> userManager, IFMPService fMPService)
        {
            _commentInterface = commentInterface;
            _stockInterface = stockInterface;
            _userManager = userManager;
            _fMPService = fMPService;
        }
        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] CommentQueryObject queryObject)
        {
            var comments = await _commentInterface.GetCommentsAsync(queryObject);

            var commentDto = comments.Select(s => s.ToCommentDtoFromComment());
            return Ok(commentDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentInterface.GetByIdAsync(id);
            if (comment == null) return BadRequest("comment not found");

            return Ok(comment.ToCommentDtoFromComment());

        }
        [Authorize]
        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, WriteCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockInterface.FindBySymbolAsync(symbol);
            //IF the stock doesnot exist then it creates the stock
            if(stock == null)
            {
                var stockFromFMP = await _fMPService.FindStockBySymbolAsync(symbol);
                if(stockFromFMP == null)
                {
                    return BadRequest("stock doesnot exists");
                }
                 _stockInterface.Create(stockFromFMP);

            }

            //If stock already exists then this runs 
            var userName = User.GetUserId();
            var appUser = await _userManager.FindByEmailAsync(userName);

            var commentModel = commentDto.ToCommentFromWrite(stock.Id, appUser);
            var result = await _commentInterface.WriteCommentAsync(commentModel);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.ToCommentDtoFromComment());

            }
            else return BadRequest();


        }
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            var result = await _commentInterface.DeleteCommentAsync(commentId);
            if (result == false) return BadRequest("Comment Does not exist");
            return Ok("Comment Deleted SuccessFully");
        }
        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, UpdateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cExists = await _commentInterface.CommentExistsAsync(commentId);
            if (cExists == null) return NotFound("Could not find specified comment");

            var commentModel = await _commentInterface.UpdateCommentAsync(cExists, dto);

            var currUser = HttpContext.User.GetUserId();
            var appuser = await _userManager.FindByEmailAsync(currUser);

            commentModel.AppUser = appuser;

            return Ok(commentModel.ToCommentDtoFromComment());
        }
    }
}
