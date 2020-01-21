using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class DbPaysRepository : IPaysRepository
    {
        private EntitiesContext.WebzineDbContext _dbContext = new EntitiesContext.WebzineDbContext();

       
        /// <summary>
        /// Ajoute un Pays dans le set de données
        /// </summary>
        /// <param name="Pays">Pays à ajouter</param>
        void IPaysRepository.Add(Pays pays)
        {
            _dbContext.Pays.Add(pays);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Supprime un Pays du set de données
        /// </summary>
        /// <param name="Pays">Pays à supprimer</param>
        void IPaysRepository.Delete(Pays pays)
        {
            if (!_dbContext.Artistes.Any(t => t.Pays.IdPays != pays.IdPays))
            {
                _dbContext.Pays.Remove(pays);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Envoie le nombre d'pays contenus dans le set de données
        /// </summary>
        /// <returns>Nombre d'pays dans le set</returns>
        int IPaysRepository.Count()
        {
            return _dbContext.Pays.Count();
        }

        /// <summary>
        /// Renvoie l'Pays correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID de l'Pays recherché</param>
        /// <returns>Pays correspondant</returns>
        Pays IPaysRepository.Find(int id)
        {
            return _dbContext.Pays.Where(a => a.IdPays == id).Include(a => a.Artistes).FirstOrDefault();
        }

        /// <summary>
        /// Récupère tout les pays contenus dans le set de données
        /// </summary>
        /// <returns>Pays présents dans le set</returns>
        IEnumerable<Pays> IPaysRepository.FindAll()
        {
            return _dbContext.Pays.ToList();
        }

        /// <summary>
        /// Retourne une fourchette d'pays
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Pays> IPaysRepository.Take(int index, int length)
        {
            return _dbContext.Pays.OrderBy(x => x.Nom.ToLower()).Skip(index).Take(length);
        }

        /// <summary>
        /// Met à jour un Pays dans le set de données
        /// </summary>
        /// <param name="Pays">Pays modifié</param>
        void IPaysRepository.Update(Pays pays)
        {
            _dbContext.Entry(_dbContext.Pays.FirstOrDefault(a => a.IdPays == pays.IdPays)).CurrentValues.SetValues(pays);
            _dbContext.SaveChanges();
        }
    }
}
