// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using StrategyCorps.CodeSample.Interfaces.Core;
using StructureMap;

namespace StrategyCorps.CodeSample.WebApi.DependencyResolution {
    /// <summary>
    /// IoC
    /// </summary>
    public static class IoC {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <returns></returns>
        public static IContainer Initialize() {
            var container = new Container(c =>
            {
                c.AddRegistry<DefaultWebApiRegistry>();
            });

            foreach (var startup in container.GetAllInstances<IStartUp>())
            {
                startup.Execute(container);
            }

            //uncomment to debug container
            //var what = container.WhatDoIHave();
            //Debug.Write(what);

            return container;
        }
    }
}