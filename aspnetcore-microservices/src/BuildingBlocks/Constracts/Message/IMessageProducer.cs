﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.Message
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
