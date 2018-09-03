using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    public class PersonBusiness
    {
        readonly PersonDao _personDao;

        public PersonBusiness(MardisContext mardisContext)
        {
            _personDao = new PersonDao(mardisContext);
        }

        /// <summary>
        /// Obtener las personas con status activo
        /// </summary>
        /// <returns></returns>
        public List<Person> GetActivePersons()
        {
            return _personDao.GetActivePersons();
        }

        /// <summary>
        /// Obtener la persona por tipo de documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Person GetPersonByDocument(string document)
        {
            return _personDao.GetPersonByDocument(document);
        }

        /// <summary>
        /// Obtiene el Id de Una Persona
        /// </summary>
        /// <param name="document">Documento de la Persona</param>
        /// <param name="typeDocument">Tipo de Documento de la Persona</param>
        /// <returns>Id de la Persona</returns>
        public Guid GetIdPersonByDocumentAndTypeDocument(string document, string typeDocument)
        {
            return _personDao.GetIdPersonByDocumentAndTypeDocument(document, typeDocument);
        }

        public bool CreatePerson(Person person)
        {
            return _personDao.CreatePerson(person);
        }
    }
}
