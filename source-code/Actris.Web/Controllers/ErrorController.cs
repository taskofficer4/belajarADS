﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Actris.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}