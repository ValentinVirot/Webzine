using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    public interface IPaysRepository
    {
        /// <summary>
        /// Ajoute un pays dans le set de données
        /// </summary>
        /// <param name="pays">Pays à ajouter</param>
        public void Add(Pays pays);

        /// <summary>
        /// Supprime un pays du set de données
        /// </summary>
        /// <param name="pays">Pays à supprimer</param>
        public void Delete(Pays pays);

        /// <summary>
        /// Envoie le nombre de pays contenus dans le set de données
        /// </summary>
        /// <returns>Nombre de pays dans le set</returns>
        public int Count();

        /// <summary>
        /// Renvoie le pays correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID du pays recherché</param>
        /// <returns>Pays correspondant</returns>
        public Pays Find(int id);


        /// <summary>
        /// Récupère tout les pays contenus dans le set de données
        /// </summary>
        /// <returns>Pays présents dans le set</returns>
        public IEnumerable<Pays> FindAll();

        /// <summary>
        /// Retourne une fourchette de pays
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        public IEnumerable<Pays> Take(int index, int length);

        /// <summary>
        /// Met à jour un pays dans le set de données
        /// </summary>
        /// <param name="pays">Pays modifié</param>
        public void Update(Pays pays);

    }
}
