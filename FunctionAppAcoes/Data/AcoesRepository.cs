using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Driver;
using FunctionAppAcoes.Models;
using FunctionAppAcoes.Documents;

namespace FunctionAppAcoes.Data
{
    public class AcoesRepository
    {
        private readonly IMapper _mapper;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<AcaoDocument> _collection;

        public AcoesRepository(IMapper mapper)
        {
            _mapper = mapper;
            _client = new MongoClient(
                Environment.GetEnvironmentVariable("MongoConnection"));
            _db = _client.GetDatabase(
                Environment.GetEnvironmentVariable("MongoDatabase"));
            _collection = _db.GetCollection<AcaoDocument>(
                Environment.GetEnvironmentVariable("MongoCollection"));
        }

        public void Save(Acao acao)
        {
            _collection.InsertOne(_mapper.Map<AcaoDocument>(acao));
        }

        public List<Acao> ListAll()
        {
            return _mapper.Map<List<Acao>>(
                _collection.Find(all => true).ToEnumerable()
                .OrderByDescending(d => d.Data).ToList());
        }
    }
}