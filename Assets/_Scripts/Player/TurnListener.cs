﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.Player
{
    public interface TurnListener
    {

        void OnNextTurn(PlayerComponent oldPlayer, PlayerComponent newPlayer);

    }
}
