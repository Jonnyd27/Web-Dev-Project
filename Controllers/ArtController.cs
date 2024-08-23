using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Dev_Project.Data;
using Web_Dev_Project.Models;

namespace Web_Dev_Project.Controllers
{
    //[Route("/")]
    public class ArtController : Controller
    {
        private MyDBContext db;
        //This is the name of where all the art is stored.
        List<Art> ArtGallary = new List<Art>();
        //This is my constructor, info will be transfered to a data base.
        public ArtController(MyDBContext db2)
        {
            db = db2;
        }
        public IActionResult Index() //This method Is my Index that I plan to have all of my links
        {
            return View("ListAll", db.ArtTable.ToList());
        }
        
        public IActionResult Home()//This is the main page to display Amelias work and catch the eye
        {
            return View();
        }

        public IActionResult About()//This gives personability to Amelia, maybe we will add family pictures
        {
            return View();
        }

        public IActionResult ArtInfo(int id)//This is Amelia Portfolio
        {
            Art a = db.ArtTable.Find( id);

            if (a != null)
                return View(a);

            //we only get here is the shoe.id was not found
            return NotFound();//404 error - page not found
        }

        public IActionResult ListAll()
        {
            return View(db.ArtTable.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Art newart)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.ArtTable.Add(newart);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //This is the get method for my Edit Action
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var a =  db.ArtTable.Find( id );
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }
        //This is the get method for my Edit Action
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("PieceName, Description, Id")] Art a)
        {
            
            if (id != a.Id)
            {
                return View(a);
            }
            if (ModelState.IsValid)
            {
                
                
                db.Update(a);
                await db.SaveChangesAsync();
                
                return RedirectToAction(nameof(Home));
            }
            return View(a);
        }

        public IActionResult Delete(int id)
        {
            var a = db.ArtTable.Find(id);
            if(a!= null)
            {
                db.ArtTable.Remove(a);
                db.SaveChangesAsync();
            }
            
            return RedirectToAction("ListAll");
        }

    //All of these actions have a view page that will point to each other and give meaningfull information
}
}
