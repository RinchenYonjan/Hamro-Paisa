using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HamroPaisa.HamroPaisaCollection.Models
{

    public class PaisaModel
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Dept")]
        public decimal Dept { get; set; }

        [JsonPropertyName("Saving")]
        public decimal Saving { get; set; }

    }


}




  

           
