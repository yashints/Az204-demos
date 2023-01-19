//   
//   Copyright © Microsoft Corporation, All Rights Reserved
// 
//   Licensed under the Apache License, Version 2.0 (the "License"); 
//   you may not use this file except in compliance with the License. 
//   You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0 
// 
//   THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
//   OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION
//   ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A
//   PARTICULAR PURPOSE, MERCHANTABILITY OR NON-INFRINGEMENT.
// 
//   See the Apache License, Version 2.0 for the specific language
//   governing permissions and limitations under the License. 

namespace MessagingSamples
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;


  // This is a common entry point class for all samples. It loads the properties
  // stored in the "azure-msg-samples.properties" file from the user profile
  // and then allows override of the settings from environment variables.
  public class Sample
  {
    public const string BasicQueueName = "BasicQueue";
    static readonly string samplePropertiesFileName = "azure-msg-config.properties";
    static readonly string SB_RECEIVE_CONNECTIONSTRING = "Endpoint=sb://az204sbdemo.servicebus.windows.net/;SharedAccessKeyName=Get;SharedAccessKey=C38Emwwy4QV8/0/Tw/TGBe+GG4yujoVtgqTrBCIZlP4=;EntityPath=basicqueue";
    static readonly string SB_SEND_CONNECTIONSTRING = "Endpoint=sb://az204sbdemo.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=pQifWqxPViuAWmsJClw2NBVWpbALvtUunteVkyu4AjQ=;EntityPath=basicqueue";

#if STA
        [STAThread]
#endif
    [DebuggerStepThrough]
    public void RunSample(string[] args, Func<string, string, Task> run)
    {


      run(SB_SEND_CONNECTIONSTRING, SB_RECEIVE_CONNECTIONSTRING).GetAwaiter().GetResult();
    }
  }

}
