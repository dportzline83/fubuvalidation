﻿using System.Linq;
using FubuCore.Reflection;
using FubuTestingSupport;
using FubuValidation.Fields;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuValidation.Tests
{
    [TestFixture]
    public class ValidationGraphTester
    {
        private SelfValidatingClassRuleSource theSelfValidatingSource;
        private ValidationGraph theGraph;
        private FieldRulesRegistry theFieldRegistry;

        [SetUp]
        public void SetUp()
        {
            theSelfValidatingSource = new SelfValidatingClassRuleSource();

            theFieldRegistry = new FieldRulesRegistry(new IFieldValidationSource[0], new TypeDescriptorCache());
            theGraph = new ValidationGraph(theFieldRegistry, new IValidationSource[0]);
        }

        [Test]
        public void adds_the_SelfValidatingClassRuleSource_by_default()
        {
            theGraph.Sources.ShouldContain(theSelfValidatingSource);
        }

        [Test]
        public void adds_the_field_rule_source_by_default()
        {
            theGraph.Sources.ShouldContain(s => s is FieldRuleSource);
        }

        [Test]
        public void adds_the_source()
        {
            var theSource = MockRepository.GenerateStub<IValidationSource>();
            theGraph.RegisterSource(theSource);
            theGraph.Sources.ShouldContain(theSource);
        }
    }
}