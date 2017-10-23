using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RockPaperScissor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitName(string name)
        {
            HttpContext.Session.SetString("message", "Ready!");
            HttpContext.Session.SetString("name", name);
            return RedirectToAction("Game");
        }
        
        [HttpGet]
        public IActionResult Game()
        {
            ViewBag.message = HttpContext.Session.GetString("message");
            int? totalGame = HttpContext.Session.GetInt32("totalGame");
            if (totalGame == null)
            {
                totalGame = 0;
            }
            int? gameWon = HttpContext.Session.GetInt32("gameWon");
            if (gameWon == null)
            {
                gameWon = 0;
            }
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.totalGame = totalGame;
            ViewBag.gameWon = gameWon;
            return View();
        }

        [HttpPost]
        public IActionResult GameLogic(string name)
        {
            Random rand = new Random();
            List<string> list = new List<string>{"rock", "paper", "scissors"};
            string result = list[rand.Next(0,3)];
            int? gameWon = HttpContext.Session.GetInt32("gameWon");
            int? totalGame = HttpContext.Session.GetInt32("totalGame");
            totalGame++;
            HttpContext.Session.SetInt32("totalGame", (int)totalGame);
            if (result == "rock")
            {
                if (name == "rock")
                {
                    HttpContext.Session.SetString("message", "You Draw");
                }
                if (name == "paper")
                {
                    gameWon++;                    HttpContext.Session.SetString("message", "You Won");
                }
                if (name == "scissors")
                {
                    HttpContext.Session.SetString("message", "You lost...");
                }
            }
            else if (result == "paper")
            {
                if (name == "paper")
                {
                    HttpContext.Session.SetString("message", "You Draw");
                }
                if (name == "scissors")
                {
                    gameWon++;                    HttpContext.Session.SetString("message", "You Won");
                }
                if (name == "rock")
                {
                    HttpContext.Session.SetString("message", "You lost...");
                }
            }
            else
            {
                if (name == "scissors")
                {
                    HttpContext.Session.SetString("message", "You Draw");
                }
                if (name == "rock")
                {
                    gameWon++;                    HttpContext.Session.SetString("message", "You Won");
                }
                if (name == "paper")
                {
                    HttpContext.Session.SetString("message", "You lost...");
                }
            }
            return RedirectToAction("Game");
        }

        [HttpGet]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
