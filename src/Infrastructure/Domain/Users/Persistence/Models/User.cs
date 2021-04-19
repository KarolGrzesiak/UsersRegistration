using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Domain.Users.Persistence.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public uint BuildingNumber { get; set; }
        public string ZipCode { get; set; }
        [BsonIgnoreIfNull]
        public string FavouriteFootballTeam { get; set; }
        [BsonIgnoreIfNull]
        public string PersonalNumber { get; set; }
    }
}