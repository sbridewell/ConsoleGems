// <copyright file="GlobalSuppressions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "StyleCop.CSharp.ReadabilityRules",
    "SA1124:DoNotUseRegions",
    Justification = "Regions are used for logical grouping of methods.")]
[assembly: SuppressMessage(
    "Info Code Smell",
    "S1135:Track uses of \"TODO\" tags",
    Justification = "TODOs are tracked in the task list")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S6608:Prefer indexing instead of \"Enumerable\" methods on types implementing \"IList\"",
    Justification = "Easier to read, and performance is not an issue here.")]
[assembly: SuppressMessage(
    "Code Quality",
    "IDE0028:Collection initialization can be simplified",
    Justification = "Just results in more warnings and info messages")]
[assembly: SuppressMessage(
    "Style",
    "IDE0306:Collection initialization can be simplified",
    Justification = "Just results in more warnings and info messages")]
