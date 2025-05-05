//purpose of SportsDbSerivce.cs is to connect your ASP.NET backend to TheSportsDB API to fetch team info using a team name
using System.Net.Http;   //need this to make http requests
using System.Threading.Tasks; //for using async/await
using Microsoft.Extensions.Configuration; //Allows for reading from appsettings.JSON

//creating this so we can work @netflix one day

namespace DzoneBackend.Services
{
    public class SportsDbService{ 


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config; 
        //we are creating an object that will be used to HTTPclient's instance and configuration

        public SportsDbService (HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient; //injected HttpClient for sending web requests
            _config = config;         //injected config to access appsettings.json

        }

        public async Task<string> GetTeamInfoAsync(string teamName){ //Public method to get team infor by team name (Ex: Detroit_Lions)

        string apiKey = _config["TheSportsDB:ApiKey"];
        //pull API key from appsettings.json

        string url = $"https://www.thesportsdb.com/api/v1/json/{apiKey}/searchteams.php?t={teamName}";
        //format the full URL to call TheSportsDB API

        var response = await _httpClient.GetAsync(url);
        //send the GET request to the API

        response.EnsureSuccessStatusCode();
        //throw an error if the status code isnt 200 OK

        return await response.Content.ReadAsStringAsync();
        //read and return the raw JSON response body as a string
        }

    }
}
//the main role of this file is to talk to SportsDB and bring us back data about a sports team when you give it a name like "Detroit_Lions)  it works as a middleman between our app and the sports database