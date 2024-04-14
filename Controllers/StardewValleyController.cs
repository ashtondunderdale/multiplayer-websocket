using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Http.OData.Query;

namespace stardew_valley_api;

[ApiController]
[Route("[controller]")]
public class Fish : ControllerBase
{
    [HttpGet(Name = "Fish")]
    public dynamic GetFish()
    {
        string path = "C:\\Users\\adunderdale\\Projects\\personal\\stardew-valley-api\\Data\\Fish.json";

        string jsonContents = System.IO.File.ReadAllText(path);

        return jsonContents;
    }
}