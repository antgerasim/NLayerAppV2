//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using Microsoft.Samples.NLayerApp.Domain.Seedwork;

namespace Domain.Seedwork.Tests.Classes
{

   internal class SelfReference : ValueObject<SelfReference>
   {

      public SelfReference()
      {
      }

      public SelfReference(SelfReference value)
      {
         Value = value;
      }

      public SelfReference Value { get; set; }

   }

}