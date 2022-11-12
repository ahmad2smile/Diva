// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
//
// namespace SourceGenerators.Templates.RestApi;
//
// [ApiController]
// [Route("[controller]")]
// public class NameController: ControllerBase
// {
//     private readonly ILogger<NameController> _logger;
//
//     public NameController(ILogger<NameController> logger)
//     {
//         _logger = logger;
//     }
//     
//     [HttpGet]
//     public IEnumerable<PlaceholderModel> Get()
//     {
//         _logger.LogInformation("Example log here from get request");
//
//         return Enumerable.Range(1, 5).Select(index => new PlaceholderModel
//         {
//             Id = Guid.NewGuid()
//         });
//     }
// }