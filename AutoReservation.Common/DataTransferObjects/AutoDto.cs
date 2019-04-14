using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase
    {

        private int id;
        private AutoKlasse autoKlasse;
        private string marke;
        private int basistarif;
        private int tagestarif;

        [DataMember]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get
            {
                return autoKlasse;
            }
            set
            {
                if (autoKlasse != value)
                {
                    autoKlasse = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public string Marke
        {
            get
            {
                return marke;
            }
            set
            {
                if (marke != value)
                {
                    marke = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public int Basistarif
        {
            get
            {
                return basistarif;
            }
            set
            {
                if (basistarif != value)
                {
                    basistarif = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public int Tagestarif
        {
            get
            {
                return tagestarif;
            }
            set
            {
                if (tagestarif != value)
                {
                    tagestarif = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                AutoKlasse = AutoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                Id,
                Marke,
                Tagestarif,
                Basistarif,
                AutoKlasse);
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to AutoDto return false.
            AutoDto other = obj as AutoDto;
            if ((System.Object)other == null)
            {
                return false;
            }

            return this.Id == other.Id
                && this.Marke == other.Marke
                && this.AutoKlasse == other.AutoKlasse
                && this.Basistarif == other.Basistarif
                && this.Tagestarif == other.Tagestarif;
        }

    }
}
