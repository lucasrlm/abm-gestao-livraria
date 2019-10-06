﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace transacao_cartao_api.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string CardHolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int UserId { get; set; }
        public int SecurityCode { get; set; }
    }
}
