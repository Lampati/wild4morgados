﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;

namespace WebProgramAR.Controllers
{
    public class EjercicioController : Controller
    {
        //
        // GET: /Ejercicio/

        public ActionResult Index()
        {
            List<Ejercicio> ejercicios = EjercicioNegocio.ObtenerPagina(1, 1, "", 0, "").ToList();
            List<NivelEjercicio> nivelEjercicios = NivelEjercicioNegocio.ObtenerPagina(1, 1, "", 0, "").ToList();
            List<EstadoEjercicio> estadoEjercicios = EstadoEjercicioNegocio.ObtenerPagina(1, 1, "", 0, "").ToList();
            return View();
        }

        //
        // GET: /Ejercicio/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Ejercicio/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Ejercicio/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Ejercicio/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Ejercicio/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Ejercicio/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Ejercicio/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
