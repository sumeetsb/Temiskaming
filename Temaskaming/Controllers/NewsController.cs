﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class NewsController : Controller
    {

        NewsClass objNews = new NewsClass();
        public ActionResult Index()
        {
            var news = objNews.getNews();
            return View(news);
        }

        public ActionResult newsadmin()
        {
            var news = objNews.getNews();
            return View(news);
        }

        public ActionResult newsupdate(int id)
        {
            var news = objNews.GetNewsByID(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }
            
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult newsupdate(int id, news news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.NewsUpdate(id, news.title, news.content);
                    return RedirectToAction("newsadmin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult newsdelete(int id)
        {
            var news = objNews.GetNewsByID(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }
        }

        [HttpPost]
        public ActionResult newsdelete(int id, news news)
        {
            try
            {
                objNews.NewsDelete(id);
                return RedirectToAction("newsadmin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult newsinsert()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult newsinsert(news news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.NewsInsert(news);
                    return RedirectToAction("newsadmin");
                }

                catch
                {
                    return View();
                }
            }

            else
            {
                return View();
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}
