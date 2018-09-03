using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class PersonDao : ADao
    {
        public PersonDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        /// <summary>
        /// Obtener las personas con status activo
        /// </summary>
        /// <returns></returns>
        public List<Person> GetActivePersons()
        {
            return Context.Persons
                .Where(p => p.StatusRegister == CStatusRegister.Active)
                .ToList();
        }

        /// <summary>
        /// Obtener la persona por tipo de documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Person GetPersonByDocument(string document)
        {
            return Context.Persons
                    .FirstOrDefault(tb => tb.Document == document &&
                                    tb.StatusRegister == CStatusRegister.Active);
        }

        /// <summary>
        /// Obtiene el Id de Una Persona
        /// </summary>
        /// <param name="document">Documento de la Persona</param>
        /// <param name="typeDocument">Tipo de Documento de la Persona</param>
        /// <returns>Id de la Persona</returns>
        public Guid GetIdPersonByDocumentAndTypeDocument(string document, string typeDocument)
        {
            return Context.Persons
                .Where(p => p.Document == document &&
                            p.TypeDocument == typeDocument &&
                            p.StatusRegister == CStatusRegister.Active)
                .Select(p => p.Id)
                .FirstOrDefault();
        }

        public bool CreatePerson(Person person)
        {
            try
            {
                Context.Add(person);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Dame Una Persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetOne(Guid id)
        {
            return Context.Persons
                .FirstOrDefault(tb => tb.Id == id &&
                            tb.StatusRegister == CStatusRegister.Active);

        }

        /// <summary>
        /// Dame persona por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Person GetPersonByCode(string code)
        {
            return Context.Persons
                          .FirstOrDefault(tb => tb.Code == code &&
                                 tb.StatusRegister == CStatusRegister.Active);
        }


        /// <summary>
        /// Obtener las personas con status activo
        /// </summary>
        /// <returns></returns>
        public List<Pollster> GetActiveIMEI(Guid idaccount)
        {
            return Context.Pollsters
                .Where(p => p.Status == CStatusRegister.Active )
                .ToList();

        }

    }
}
