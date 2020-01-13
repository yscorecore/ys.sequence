﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.Sequence
{
    public interface ISequenceService
    {
        Task<long> GetValueAsync(string name);
        Task CreateSequence(string name, SequenceInfo sequenceInfo);
        Task<long> GetValueOrCreateAsync(string name, SequenceInfo sequenceInfo);
        Task ResetAsync(string name);
        Task<bool> RemoveAsync(string name);
        Task<bool> ExistsAsync(string name);
    }
}