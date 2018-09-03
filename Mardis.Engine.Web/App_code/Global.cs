using System;
using LoginViewModel = Mardis.Engine.Web.ViewModel.LoginViewModel;

namespace Mardis.Engine.Web.App_code
{
    public static class Global
    {
        /// <summary>
        /// Variable global que almacena cosas importantes.
        /// </ summary>
        static LoginViewModel _importantData;
        static Guid _Iduser;
        static Guid _ProfileId;

        static Guid _AccountId;

        static Guid _PersonId;

        /// <summary>
        /// Obtener o establecer los datos importantes estáticos.
        /// </ summary>
        public static LoginViewModel ImportantData
        {
            get
            {
                return _importantData;
            }
            set
            {
                _importantData = value;
            }
        }
        public static Guid UserID
        {
            get
            {
                return _Iduser;
            }
            set
            {
                _Iduser = value;
            }
        }
        public static Guid ProfileId
        {
            get
            {
                return _ProfileId;
            }
            set
            {
                _ProfileId = value;
            }
        }
        public static Guid AccountId
        {
            get
            {
                return _AccountId;
            }
            set
            {
                _AccountId = value;
            }
        }
        public static Guid PersonId
        {
            get
            {
                return _PersonId;
            }
            set
            {
                _PersonId = value;
            }
        }
    }
}