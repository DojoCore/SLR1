﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace SLR_1__Parser.Models
{
    public partial class Rule
    {
        [JsonProperty("define")]
        public Symbol Define { get; set; }

        [JsonProperty("as")]
        public List<Symbol> Expression { get; set; }

        public Rule() { }

        public Rule(Rule rule)
        {
            this.Define = rule.Define;
            this.Expression = new List<Symbol>(rule.Expression.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj.GetType() == typeof(Rule))
            {
                var rule = (Rule)obj;
                if (rule.Define == this.Define)
                {
                    if (rule.Expression.Count == this.Expression.Count)
                    {
                        for (int i = 0; i < rule.Expression.Count; i++)
                        {
                            if (rule.Expression[i] != this.Expression[i])
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Define} → ");
            foreach (var s in Expression)
            {
                sb.Append($"{s}");
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            var hashCode = 915180621;
            hashCode = hashCode * -1521134295 + EqualityComparer<Symbol>.Default.GetHashCode(Define);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Symbol>>.Default.GetHashCode(Expression);
            return hashCode;
        }
    }

    public partial class Rule
    {
        public static List<Rule> FromJson(string json) => JsonConvert.DeserializeObject<List<Rule>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Grammar> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
