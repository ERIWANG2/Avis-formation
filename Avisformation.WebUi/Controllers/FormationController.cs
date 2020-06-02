using Avisformation.Data;
using Avisformation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avisformation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {
            List<Formation> listFormations = new List<Formation>();

            using (var context = new AvisEntities())
            {
                listFormations = context.Formation.ToList();
            }            
            

            return View(listFormations);
        }

        //Affichage de la page détail formation moyenant le nom de la formation
        public ActionResult DetailsFormation(string nomSeo) {
            //création d'un viewModel(Qui est un objet intermédiaire en la vue et le controller
            var vm = new FormationAvecAvisViewModel();

            using (var context = new AvisEntities())
            {   //si la valeur n'est pas trouvée par défaut elle renvoie nulle
                var formationEntity = context.Formation.Where(f=>f.NomSeo ==nomSeo).FirstOrDefault();
                if (formationEntity == null)
                    return RedirectToAction("Accueil","home");

                //affectation des données dans vm
                vm.FormationName = formationEntity.Nom;
                vm.FormationDescription = formationEntity.Description;
                vm.FormationNom = formationEntity.Nom;
                vm.FormationNomSeo = formationEntity.NomSeo;
                vm.FormationUrl = formationEntity.Url;
                vm.Note = formationEntity.Avis.Average(a=>a.Note);
                vm.NombreAvis = formationEntity.Avis.Count;
                vm.Avis = formationEntity.Avis.ToList();

            }

            return View(vm);
        }


    }
}       