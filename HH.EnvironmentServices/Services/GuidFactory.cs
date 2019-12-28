using System;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public sealed class GuidFactory : IGuidFactory
    {
        public Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
