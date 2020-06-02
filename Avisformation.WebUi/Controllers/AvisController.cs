using Avisformation.Data;
using Avisformation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avisformation.WebUi.Controllers
{
    public class AvisController : Controller
    {

        //méthode permettant d'afficher la enetre laisser un avis
        //selon la formation dont le nom est envoyé en paramètre

        public ActionResult LaisserUnAvis(string nomSeo) {
            var vm = new LaisserUnAvisViewModel();
            vm.FormationNomSeo = nomSeo;

            using (var context = new AvisEntities())
            {
                //recherche de la formation via le nomSeo
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                //test si l'entité est null cad pas des données
                if (formationEntity == null)
                    return RedirectToAction("Accueil", "Home");
                vm.FormationName = formationEntity.Nom;
            }
            return View(vm);
        }

        //méthode pour enregistrer les avis
        public ActionResult SaveCommentaire(string commentaire,string nom,string note,string nomSeo) {
            //créons une nouvelle avis
            Avis nouvelAvis = new Avis();
            nouvelAvis.DateAvis = DateTime.Now;
            nouvelAvis.Description = commentaire;           
            nouvelAvis.Nom = nom;

            double dnote = 0;

            if(!double.TryParse(note,NumberStyles.Any,CultureInfo.InvariantCulture, out dnote)){
                throw new Exception("Impossible de passer la note" + note);
            }
            nouvelAvis.Note = dnote;
            //récupération du context
            using (var context = new AvisEntities())
            {
                //recherche de la formation via le nomSeo
                var formationEntity=context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                if (formationEntity == null)
                    return RedirectToAction("Accueil","Home");

                nouvelAvis.IdFormation = formationEntity.Id;
                context.Avis.Add(nouvelAvis);
                context.SaveChanges();
            }
            return View();
        }

    }
}