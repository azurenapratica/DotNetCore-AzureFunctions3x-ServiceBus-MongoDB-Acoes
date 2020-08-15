using MongoDB.Bson;

namespace FunctionAppAcoes.Documents
{
    public class AcaoDocument
    {
        public ObjectId _id { get; set; }
        public string HistLancamento { get; set; }
        public string Sigla { get; set; }
        public string Data { get; set; }
        public double Valor { get; set; }
    }
}