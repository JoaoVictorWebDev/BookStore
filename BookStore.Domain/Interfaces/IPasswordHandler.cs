﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces;

public interface IPasswordHandler
{
    public string Hash(string password);
}
