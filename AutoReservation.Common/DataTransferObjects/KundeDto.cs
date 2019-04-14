using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {

        private DateTime geburtsdatum;
        private int id;
        private string nachname;
        private string vorname;

        [DataMember]
        public DateTime Geburtsdatum { 
            get{
                return geburtsdatum;
            }
            
            set{

                if (geburtsdatum != value)
                {
                    geburtsdatum = value;
                    RaisePropertyChanged();
                }
            }
        }

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
        public string Nachname
        {
            get
            {
                return nachname;
            }

            set
            {
                if (nachname != value)
                {
                    nachname = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public string Vorname
        {
            get
            {
                return vorname;
            }

            set
            {
                if (vorname != value)
                {
                    vorname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new KundeDto
            {
                Id = Id,
                Nachname = Nachname,
                Vorname = Vorname,
                Geburtsdatum = Geburtsdatum
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}",
                Id,
                Nachname,
                Vorname,
                Geburtsdatum);
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to KundeDto return false.
            KundeDto other = obj as KundeDto;
            if ((System.Object)other == null)
            {
                return false;
            }

            return this.Id == other.Id
                && this.Nachname == other.Nachname
                && this.Vorname == other.Vorname
                && this.Geburtsdatum == other.Geburtsdatum;
        }

    }
}
