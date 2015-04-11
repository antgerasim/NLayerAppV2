// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly:
   SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Crosscutting",
      Scope = "namespace", Target = "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator")]
[assembly:
   SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Crosscutting",
      Scope = "namespace", Target = "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Crosscutting")
]
[assembly:
   SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
      Target =
         "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging.TraceSourceLog.#LogError(System.String,System.Exception,System.Object[])"
      )]
[assembly:
   SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
      Target = "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator")]
[assembly:
   SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
      Target = "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging")]
[assembly:
   SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
      Target =
         "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator.DataAnnotationsEntityValidator.#SetValidatableObjectErrors`1(!!0,System.Collections.Generic.List`1<System.String>)"
      )]
[assembly:
   SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
      Target =
         "Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator.DataAnnotationsEntityValidator.#SetValidationAttributeErrors`1(!!0,System.Collections.Generic.List`1<System.String>)"
      )]