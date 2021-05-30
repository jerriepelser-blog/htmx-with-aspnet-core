using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmxDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxDemo.Pages
{
    public class InfiniteScrolling : PageModel
    {
        private readonly Database _database;

        public List<Tweet> InitialTweets { get; set; }

        public InfiniteScrolling(Database database)
        {
            _database = database;
        }
        
        public void OnGet()
        {
            InitialTweets = _database.Tweets.OrderByDescending(t => t.Time).Take(20).ToList();
        }

        public async Task<IActionResult> OnGetLoadTweets(DateTime after)
        {
            var next20Tweets = _database.Tweets
                .OrderByDescending(t => t.Time)
                .Where(t => t.Time < after)
                .Take(20)
                .ToList();

            // Artificial delay to simulate it taking some time to load the next set of records
            // For goodness sake, don't add this to your production code :D
            await Task.Delay(2000);  
            
            return Partial("_TweetList", next20Tweets);
        }
    }
}