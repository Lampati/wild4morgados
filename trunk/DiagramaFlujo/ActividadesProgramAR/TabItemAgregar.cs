﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    public class TabItemAgregar : Tab
    {
        public override void Ejecutar()
        {
            //no hace nada este tab
        }

        protected override void RecrearWorkflowDesigner()
        {
            //no hago nada, no tengo que recrear el designer
        }
    }
}