﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class WaittimeController : Controller
    {
        WaittimeClass objWaittime = new WaittimeClass();

        public ActionResult Index()
        {
            List<SelectListItem> patientNumList = new List<SelectListItem>();
            patientNumList.Add(new SelectListItem
            {Text = "5", Value = "5"});
            patientNumList.Add(new SelectListItem
            {Text = "10", Value = "10"});
            patientNumList.Add(new SelectListItem
            {Text = "15", Value = "15"});
            ViewBag.numList = patientNumList;
            ViewBag.Wait_Time = Math.Round(objWaittime.GetWaitTime(5)).ToString();
            ViewBag.waitingPatients = objWaittime.GetWaitingPatients().ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<SelectListItem> patientNumList = new List<SelectListItem>();
            patientNumList.Add(new SelectListItem { Text = "5", Value = "5" });
            patientNumList.Add(new SelectListItem { Text = "10", Value = "10" });
            patientNumList.Add(new SelectListItem { Text = "15", Value = "15" });
            ViewBag.numList = patientNumList;
            int numPatient = Int32.Parse(form["NumPatients"]);
            ViewBag.PatientNumber = numPatient;
            ViewBag.Wait_Time = Math.Round(objWaittime.GetWaitTime(numPatient), 0).ToString();
            ViewBag.waitingPatients = objWaittime.GetWaitingPatients().ToString();
            return View();
        }

        public ActionResult admin()
        {
            ViewBag.Group = "Admin";
            var patients = objWaittime.GetPatients();
            return View(patients);
        }

        public ActionResult update(int id)
        {
            ViewBag.Group = "Admin";
            var patient = objWaittime.GetPatientByID(id);
            return View(patient);
        }

        [HttpPost]
        public ActionResult update(int id, waittime patient)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objWaittime.PatientUpdate(id, patient.name, patient.time_admit, patient.time_doctor);
                    return RedirectToAction("admin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult delete(int id)
        {
            ViewBag.Group = "Admin";
            var patient = objWaittime.GetPatientByID(id);
            if (patient == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(patient);
            }
        }

        [HttpPost]
        public ActionResult delete(int id, waittime patient)
        {
            ViewBag.Group = "Admin";
            try
            {
                objWaittime.PatientDelete(id);
                return RedirectToAction("admin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(waittime patient)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objWaittime.PatientInsert(patient);
                    return RedirectToAction("admin");
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
            ViewBag.Group = "Admin";
            return View();
        }

    }
}
