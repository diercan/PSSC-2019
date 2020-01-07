/**
 * @license
 * Copyright Google Inc. All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
(function (factory) {
    if (typeof module === "object" && typeof module.exports === "object") {
        var v = factory(require, exports);
        if (v !== undefined) module.exports = v;
    }
    else if (typeof define === "function" && define.amd) {
        define("@angular/compiler-cli/ngcc/src/host/esm2015_host", ["require", "exports", "tslib", "typescript", "@angular/compiler-cli/src/ngtsc/reflection", "@angular/compiler-cli/ngcc/src/utils", "@angular/compiler-cli/ngcc/src/host/ngcc_host"], factory);
    }
})(function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var tslib_1 = require("tslib");
    var ts = require("typescript");
    var reflection_1 = require("@angular/compiler-cli/src/ngtsc/reflection");
    var utils_1 = require("@angular/compiler-cli/ngcc/src/utils");
    var ngcc_host_1 = require("@angular/compiler-cli/ngcc/src/host/ngcc_host");
    exports.DECORATORS = 'decorators';
    exports.PROP_DECORATORS = 'propDecorators';
    exports.CONSTRUCTOR = '__constructor';
    exports.CONSTRUCTOR_PARAMS = 'ctorParameters';
    /**
     * Esm2015 packages contain ECMAScript 2015 classes, etc.
     * Decorators are defined via static properties on the class. For example:
     *
     * ```
     * class SomeDirective {
     * }
     * SomeDirective.decorators = [
     *   { type: Directive, args: [{ selector: '[someDirective]' },] }
     * ];
     * SomeDirective.ctorParameters = () => [
     *   { type: ViewContainerRef, },
     *   { type: TemplateRef, },
     *   { type: undefined, decorators: [{ type: Inject, args: [INJECTED_TOKEN,] },] },
     * ];
     * SomeDirective.propDecorators = {
     *   "input1": [{ type: Input },],
     *   "input2": [{ type: Input },],
     * };
     * ```
     *
     * * Classes are decorated if they have a static property called `decorators`.
     * * Members are decorated if there is a matching key on a static property
     *   called `propDecorators`.
     * * Constructor parameters decorators are found on an object returned from
     *   a static method called `ctorParameters`.
     */
    var Esm2015ReflectionHost = /** @class */ (function (_super) {
        tslib_1.__extends(Esm2015ReflectionHost, _super);
        function Esm2015ReflectionHost(logger, isCore, checker, dts) {
            var _this = _super.call(this, checker) || this;
            _this.logger = logger;
            _this.isCore = isCore;
            /**
             * The set of source files that have already been preprocessed.
             */
            _this.preprocessedSourceFiles = new Set();
            /**
             * In ES2015, class declarations may have been down-leveled into variable declarations,
             * initialized using a class expression. In certain scenarios, an additional variable
             * is introduced that represents the class so that results in code such as:
             *
             * ```
             * let MyClass_1; let MyClass = MyClass_1 = class MyClass {};
             * ```
             *
             * This map tracks those aliased variables to their original identifier, i.e. the key
             * corresponds with the declaration of `MyClass_1` and its value becomes the `MyClass` identifier
             * of the variable declaration.
             *
             * This map is populated during the preprocessing of each source file.
             */
            _this.aliasedClassDeclarations = new Map();
            _this.dtsDeclarationMap = dts && _this.computeDtsDeclarationMap(dts.path, dts.program) || null;
            return _this;
        }
        /**
         * Find the declaration of a node that we think is a class.
         * Classes should have a `name` identifier, because they may need to be referenced in other parts
         * of the program.
         *
         * In ES2015, a class may be declared using a variable declaration of the following structure:
         *
         * ```
         * var MyClass = MyClass_1 = class MyClass {};
         * ```
         *
         * Here, the intermediate `MyClass_1` assignment is optional. In the above example, the
         * `class MyClass {}` node is returned as declaration of `MyClass`.
         *
         * @param node the node that represents the class whose declaration we are finding.
         * @returns the declaration of the class or `undefined` if it is not a "class".
         */
        Esm2015ReflectionHost.prototype.getClassDeclaration = function (node) {
            return getInnerClassDeclaration(node) || undefined;
        };
        /**
         * Find a symbol for a node that we think is a class.
         * @param node the node whose symbol we are finding.
         * @returns the symbol for the node or `undefined` if it is not a "class" or has no symbol.
         */
        Esm2015ReflectionHost.prototype.getClassSymbol = function (declaration) {
            var classDeclaration = this.getClassDeclaration(declaration);
            return classDeclaration &&
                this.checker.getSymbolAtLocation(classDeclaration.name);
        };
        /**
         * Examine a declaration (for example, of a class or function) and return metadata about any
         * decorators present on the declaration.
         *
         * @param declaration a TypeScript `ts.Declaration` node representing the class or function over
         * which to reflect. For example, if the intent is to reflect the decorators of a class and the
         * source is in ES6 format, this will be a `ts.ClassDeclaration` node. If the source is in ES5
         * format, this might be a `ts.VariableDeclaration` as classes in ES5 are represented as the
         * result of an IIFE execution.
         *
         * @returns an array of `Decorator` metadata if decorators are present on the declaration, or
         * `null` if either no decorators were present or if the declaration is not of a decoratable type.
         */
        Esm2015ReflectionHost.prototype.getDecoratorsOfDeclaration = function (declaration) {
            var symbol = this.getClassSymbol(declaration);
            if (!symbol) {
                return null;
            }
            return this.getDecoratorsOfSymbol(symbol);
        };
        /**
         * Examine a declaration which should be of a class, and return metadata about the members of the
         * class.
         *
         * @param clazz a `ClassDeclaration` representing the class over which to reflect.
         *
         * @returns an array of `ClassMember` metadata representing the members of the class.
         *
         * @throws if `declaration` does not resolve to a class declaration.
         */
        Esm2015ReflectionHost.prototype.getMembersOfClass = function (clazz) {
            var classSymbol = this.getClassSymbol(clazz);
            if (!classSymbol) {
                throw new Error("Attempted to get members of a non-class: \"" + clazz.getText() + "\"");
            }
            return this.getMembersOfSymbol(classSymbol);
        };
        /**
         * Reflect over the constructor of a class and return metadata about its parameters.
         *
         * This method only looks at the constructor of a class directly and not at any inherited
         * constructors.
         *
         * @param clazz a `ClassDeclaration` representing the class over which to reflect.
         *
         * @returns an array of `Parameter` metadata representing the parameters of the constructor, if
         * a constructor exists. If the constructor exists and has 0 parameters, this array will be empty.
         * If the class has no constructor, this method returns `null`.
         *
         * @throws if `declaration` does not resolve to a class declaration.
         */
        Esm2015ReflectionHost.prototype.getConstructorParameters = function (clazz) {
            var classSymbol = this.getClassSymbol(clazz);
            if (!classSymbol) {
                throw new Error("Attempted to get constructor parameters of a non-class: \"" + clazz.getText() + "\"");
            }
            var parameterNodes = this.getConstructorParameterDeclarations(classSymbol);
            if (parameterNodes) {
                return this.getConstructorParamInfo(classSymbol, parameterNodes);
            }
            return null;
        };
        Esm2015ReflectionHost.prototype.hasBaseClass = function (clazz) {
            var superHasBaseClass = _super.prototype.hasBaseClass.call(this, clazz);
            if (superHasBaseClass) {
                return superHasBaseClass;
            }
            var innerClassDeclaration = getInnerClassDeclaration(clazz);
            if (innerClassDeclaration === null) {
                return false;
            }
            return innerClassDeclaration.heritageClauses !== undefined &&
                innerClassDeclaration.heritageClauses.some(function (clause) { return clause.token === ts.SyntaxKind.ExtendsKeyword; });
        };
        /**
         * Check whether the given node actually represents a class.
         */
        Esm2015ReflectionHost.prototype.isClass = function (node) {
            return _super.prototype.isClass.call(this, node) || !!this.getClassDeclaration(node);
        };
        /**
         * Trace an identifier to its declaration, if possible.
         *
         * This method attempts to resolve the declaration of the given identifier, tracing back through
         * imports and re-exports until the original declaration statement is found. A `Declaration`
         * object is returned if the original declaration is found, or `null` is returned otherwise.
         *
         * In ES2015, we need to account for identifiers that refer to aliased class declarations such as
         * `MyClass_1`. Since such declarations are only available within the module itself, we need to
         * find the original class declaration, e.g. `MyClass`, that is associated with the aliased one.
         *
         * @param id a TypeScript `ts.Identifier` to trace back to a declaration.
         *
         * @returns metadata about the `Declaration` if the original declaration is found, or `null`
         * otherwise.
         */
        Esm2015ReflectionHost.prototype.getDeclarationOfIdentifier = function (id) {
            var superDeclaration = _super.prototype.getDeclarationOfIdentifier.call(this, id);
            // The identifier may have been of an additional class assignment such as `MyClass_1` that was
            // present as alias for `MyClass`. If so, resolve such aliases to their original declaration.
            if (superDeclaration !== null) {
                var aliasedIdentifier = this.resolveAliasedClassIdentifier(superDeclaration.node);
                if (aliasedIdentifier !== null) {
                    return this.getDeclarationOfIdentifier(aliasedIdentifier);
                }
            }
            return superDeclaration;
        };
        /** Gets all decorators of the given class symbol. */
        Esm2015ReflectionHost.prototype.getDecoratorsOfSymbol = function (symbol) {
            var decoratorsProperty = this.getStaticProperty(symbol, exports.DECORATORS);
            if (decoratorsProperty) {
                return this.getClassDecoratorsFromStaticProperty(decoratorsProperty);
            }
            else {
                return this.getClassDecoratorsFromHelperCall(symbol);
            }
        };
        /**
         * Search the given module for variable declarations in which the initializer
         * is an identifier marked with the `PRE_R3_MARKER`.
         * @param module the module in which to search for switchable declarations.
         * @returns an array of variable declarations that match.
         */
        Esm2015ReflectionHost.prototype.getSwitchableDeclarations = function (module) {
            // Don't bother to walk the AST if the marker is not found in the text
            return module.getText().indexOf(ngcc_host_1.PRE_R3_MARKER) >= 0 ?
                utils_1.findAll(module, ngcc_host_1.isSwitchableVariableDeclaration) :
                [];
        };
        Esm2015ReflectionHost.prototype.getVariableValue = function (declaration) {
            var value = _super.prototype.getVariableValue.call(this, declaration);
            if (value) {
                return value;
            }
            // We have a variable declaration that has no initializer. For example:
            //
            // ```
            // var HttpClientXsrfModule_1;
            // ```
            //
            // So look for the special scenario where the variable is being assigned in
            // a nearby statement to the return value of a call to `__decorate`.
            // Then find the 2nd argument of that call, the "target", which will be the
            // actual class identifier. For example:
            //
            // ```
            // HttpClientXsrfModule = HttpClientXsrfModule_1 = tslib_1.__decorate([
            //   NgModule({
            //     providers: [],
            //   })
            // ], HttpClientXsrfModule);
            // ```
            //
            // And finally, find the declaration of the identifier in that argument.
            // Note also that the assignment can occur within another assignment.
            //
            var block = declaration.parent.parent.parent;
            var symbol = this.checker.getSymbolAtLocation(declaration.name);
            if (symbol && (ts.isBlock(block) || ts.isSourceFile(block))) {
                var decorateCall = this.findDecoratedVariableValue(block, symbol);
                var target = decorateCall && decorateCall.arguments[1];
                if (target && ts.isIdentifier(target)) {
                    var targetSymbol = this.checker.getSymbolAtLocation(target);
                    var targetDeclaration = targetSymbol && targetSymbol.valueDeclaration;
                    if (targetDeclaration) {
                        if (ts.isClassDeclaration(targetDeclaration) ||
                            ts.isFunctionDeclaration(targetDeclaration)) {
                            // The target is just a function or class declaration
                            // so return its identifier as the variable value.
                            return targetDeclaration.name || null;
                        }
                        else if (ts.isVariableDeclaration(targetDeclaration)) {
                            // The target is a variable declaration, so find the far right expression,
                            // in the case of multiple assignments (e.g. `var1 = var2 = value`).
                            var targetValue = targetDeclaration.initializer;
                            while (targetValue && isAssignment(targetValue)) {
                                targetValue = targetValue.right;
                            }
                            if (targetValue) {
                                return targetValue;
                            }
                        }
                    }
                }
            }
            return null;
        };
        /**
         * Find all top-level class symbols in the given file.
         * @param sourceFile The source file to search for classes.
         * @returns An array of class symbols.
         */
        Esm2015ReflectionHost.prototype.findClassSymbols = function (sourceFile) {
            var _this = this;
            var classes = [];
            this.getModuleStatements(sourceFile).forEach(function (statement) {
                if (ts.isVariableStatement(statement)) {
                    statement.declarationList.declarations.forEach(function (declaration) {
                        var classSymbol = _this.getClassSymbol(declaration);
                        if (classSymbol) {
                            classes.push(classSymbol);
                        }
                    });
                }
                else if (ts.isClassDeclaration(statement)) {
                    var classSymbol = _this.getClassSymbol(statement);
                    if (classSymbol) {
                        classes.push(classSymbol);
                    }
                }
            });
            return classes;
        };
        /**
         * Get the number of generic type parameters of a given class.
         *
         * @param clazz a `ClassDeclaration` representing the class over which to reflect.
         *
         * @returns the number of type parameters of the class, if known, or `null` if the declaration
         * is not a class or has an unknown number of type parameters.
         */
        Esm2015ReflectionHost.prototype.getGenericArityOfClass = function (clazz) {
            var dtsDeclaration = this.getDtsDeclaration(clazz);
            if (dtsDeclaration && ts.isClassDeclaration(dtsDeclaration)) {
                return dtsDeclaration.typeParameters ? dtsDeclaration.typeParameters.length : 0;
            }
            return null;
        };
        /**
         * Take an exported declaration of a class (maybe down-leveled to a variable) and look up the
         * declaration of its type in a separate .d.ts tree.
         *
         * This function is allowed to return `null` if the current compilation unit does not have a
         * separate .d.ts tree. When compiling TypeScript code this is always the case, since .d.ts files
         * are produced only during the emit of such a compilation. When compiling .js code, however,
         * there is frequently a parallel .d.ts tree which this method exposes.
         *
         * Note that the `ts.ClassDeclaration` returned from this function may not be from the same
         * `ts.Program` as the input declaration.
         */
        Esm2015ReflectionHost.prototype.getDtsDeclaration = function (declaration) {
            if (!this.dtsDeclarationMap) {
                return null;
            }
            if (!isNamedDeclaration(declaration)) {
                throw new Error("Cannot get the dts file for a declaration that has no name: " + declaration.getText() + " in " + declaration.getSourceFile().fileName);
            }
            return this.dtsDeclarationMap.has(declaration.name.text) ?
                this.dtsDeclarationMap.get(declaration.name.text) :
                null;
        };
        /**
         * Search the given source file for exported functions and static class methods that return
         * ModuleWithProviders objects.
         * @param f The source file to search for these functions
         * @returns An array of function declarations that look like they return ModuleWithProviders
         * objects.
         */
        Esm2015ReflectionHost.prototype.getModuleWithProvidersFunctions = function (f) {
            var _this = this;
            var exports = this.getExportsOfModule(f);
            if (!exports)
                return [];
            var infos = [];
            exports.forEach(function (declaration, name) {
                if (_this.isClass(declaration.node)) {
                    _this.getMembersOfClass(declaration.node).forEach(function (member) {
                        if (member.isStatic) {
                            var info = _this.parseForModuleWithProviders(member.name, member.node, member.implementation, declaration.node);
                            if (info) {
                                infos.push(info);
                            }
                        }
                    });
                }
                else {
                    if (isNamedDeclaration(declaration.node)) {
                        var info = _this.parseForModuleWithProviders(declaration.node.name.text, declaration.node);
                        if (info) {
                            infos.push(info);
                        }
                    }
                }
            });
            return infos;
        };
        ///////////// Protected Helpers /////////////
        /**
         * Finds the identifier of the actual class declaration for a potentially aliased declaration of a
         * class.
         *
         * If the given declaration is for an alias of a class, this function will determine an identifier
         * to the original declaration that represents this class.
         *
         * @param declaration The declaration to resolve.
         * @returns The original identifier that the given class declaration resolves to, or `undefined`
         * if the declaration does not represent an aliased class.
         */
        Esm2015ReflectionHost.prototype.resolveAliasedClassIdentifier = function (declaration) {
            this.ensurePreprocessed(declaration.getSourceFile());
            return this.aliasedClassDeclarations.has(declaration) ?
                this.aliasedClassDeclarations.get(declaration) :
                null;
        };
        /**
         * Ensures that the source file that `node` is part of has been preprocessed.
         *
         * During preprocessing, all statements in the source file will be visited such that certain
         * processing steps can be done up-front and cached for subsequent usages.
         *
         * @param sourceFile The source file that needs to have gone through preprocessing.
         */
        Esm2015ReflectionHost.prototype.ensurePreprocessed = function (sourceFile) {
            var e_1, _a;
            if (!this.preprocessedSourceFiles.has(sourceFile)) {
                this.preprocessedSourceFiles.add(sourceFile);
                try {
                    for (var _b = tslib_1.__values(sourceFile.statements), _c = _b.next(); !_c.done; _c = _b.next()) {
                        var statement = _c.value;
                        this.preprocessStatement(statement);
                    }
                }
                catch (e_1_1) { e_1 = { error: e_1_1 }; }
                finally {
                    try {
                        if (_c && !_c.done && (_a = _b.return)) _a.call(_b);
                    }
                    finally { if (e_1) throw e_1.error; }
                }
            }
        };
        /**
         * Analyzes the given statement to see if it corresponds with a variable declaration like
         * `let MyClass = MyClass_1 = class MyClass {};`. If so, the declaration of `MyClass_1`
         * is associated with the `MyClass` identifier.
         *
         * @param statement The statement that needs to be preprocessed.
         */
        Esm2015ReflectionHost.prototype.preprocessStatement = function (statement) {
            if (!ts.isVariableStatement(statement)) {
                return;
            }
            var declarations = statement.declarationList.declarations;
            if (declarations.length !== 1) {
                return;
            }
            var declaration = declarations[0];
            var initializer = declaration.initializer;
            if (!ts.isIdentifier(declaration.name) || !initializer || !isAssignment(initializer) ||
                !ts.isIdentifier(initializer.left) || !ts.isClassExpression(initializer.right)) {
                return;
            }
            var aliasedIdentifier = initializer.left;
            var aliasedDeclaration = this.getDeclarationOfIdentifier(aliasedIdentifier);
            if (aliasedDeclaration === null) {
                throw new Error("Unable to locate declaration of " + aliasedIdentifier.text + " in \"" + statement.getText() + "\"");
            }
            this.aliasedClassDeclarations.set(aliasedDeclaration.node, declaration.name);
        };
        /** Get the top level statements for a module.
         *
         * In ES5 and ES2015 this is just the top level statements of the file.
         * @param sourceFile The module whose statements we want.
         * @returns An array of top level statements for the given module.
         */
        Esm2015ReflectionHost.prototype.getModuleStatements = function (sourceFile) {
            return Array.from(sourceFile.statements);
        };
        /**
         * Walk the AST looking for an assignment to the specified symbol.
         * @param node The current node we are searching.
         * @returns an expression that represents the value of the variable, or undefined if none can be
         * found.
         */
        Esm2015ReflectionHost.prototype.findDecoratedVariableValue = function (node, symbol) {
            var _this = this;
            if (!node) {
                return null;
            }
            if (ts.isBinaryExpression(node) && node.operatorToken.kind === ts.SyntaxKind.EqualsToken) {
                var left = node.left;
                var right = node.right;
                if (ts.isIdentifier(left) && this.checker.getSymbolAtLocation(left) === symbol) {
                    return (ts.isCallExpression(right) && getCalleeName(right) === '__decorate') ? right : null;
                }
                return this.findDecoratedVariableValue(right, symbol);
            }
            return node.forEachChild(function (node) { return _this.findDecoratedVariableValue(node, symbol); }) || null;
        };
        /**
         * Try to retrieve the symbol of a static property on a class.
         * @param symbol the class whose property we are interested in.
         * @param propertyName the name of static property.
         * @returns the symbol if it is found or `undefined` if not.
         */
        Esm2015ReflectionHost.prototype.getStaticProperty = function (symbol, propertyName) {
            return symbol.exports && symbol.exports.get(propertyName);
        };
        /**
         * Get all class decorators for the given class, where the decorators are declared
         * via a static property. For example:
         *
         * ```
         * class SomeDirective {}
         * SomeDirective.decorators = [
         *   { type: Directive, args: [{ selector: '[someDirective]' },] }
         * ];
         * ```
         *
         * @param decoratorsSymbol the property containing the decorators we want to get.
         * @returns an array of decorators or null if none where found.
         */
        Esm2015ReflectionHost.prototype.getClassDecoratorsFromStaticProperty = function (decoratorsSymbol) {
            var _this = this;
            var decoratorsIdentifier = decoratorsSymbol.valueDeclaration;
            if (decoratorsIdentifier && decoratorsIdentifier.parent) {
                if (ts.isBinaryExpression(decoratorsIdentifier.parent) &&
                    decoratorsIdentifier.parent.operatorToken.kind === ts.SyntaxKind.EqualsToken) {
                    // AST of the array of decorator values
                    var decoratorsArray = decoratorsIdentifier.parent.right;
                    return this.reflectDecorators(decoratorsArray)
                        .filter(function (decorator) { return _this.isFromCore(decorator); });
                }
            }
            return null;
        };
        /**
         * Get all class decorators for the given class, where the decorators are declared
         * via the `__decorate` helper method. For example:
         *
         * ```
         * let SomeDirective = class SomeDirective {}
         * SomeDirective = __decorate([
         *   Directive({ selector: '[someDirective]' }),
         * ], SomeDirective);
         * ```
         *
         * @param symbol the class whose decorators we want to get.
         * @returns an array of decorators or null if none where found.
         */
        Esm2015ReflectionHost.prototype.getClassDecoratorsFromHelperCall = function (symbol) {
            var _this = this;
            var decorators = [];
            var helperCalls = this.getHelperCallsForClass(symbol, '__decorate');
            helperCalls.forEach(function (helperCall) {
                var classDecorators = _this.reflectDecoratorsFromHelperCall(helperCall, makeClassTargetFilter(symbol.name)).classDecorators;
                classDecorators.filter(function (decorator) { return _this.isFromCore(decorator); })
                    .forEach(function (decorator) { return decorators.push(decorator); });
            });
            return decorators.length ? decorators : null;
        };
        /**
         * Examine a symbol which should be of a class, and return metadata about its members.
         *
         * @param symbol the `ClassSymbol` representing the class over which to reflect.
         * @returns an array of `ClassMember` metadata representing the members of the class.
         */
        Esm2015ReflectionHost.prototype.getMembersOfSymbol = function (symbol) {
            var _this = this;
            var members = [];
            // The decorators map contains all the properties that are decorated
            var decoratorsMap = this.getMemberDecorators(symbol);
            // The member map contains all the method (instance and static); and any instance properties
            // that are initialized in the class.
            if (symbol.members) {
                symbol.members.forEach(function (value, key) {
                    var decorators = decoratorsMap.get(key);
                    var reflectedMembers = _this.reflectMembers(value, decorators);
                    if (reflectedMembers) {
                        decoratorsMap.delete(key);
                        members.push.apply(members, tslib_1.__spread(reflectedMembers));
                    }
                });
            }
            // The static property map contains all the static properties
            if (symbol.exports) {
                symbol.exports.forEach(function (value, key) {
                    var decorators = decoratorsMap.get(key);
                    var reflectedMembers = _this.reflectMembers(value, decorators, true);
                    if (reflectedMembers) {
                        decoratorsMap.delete(key);
                        members.push.apply(members, tslib_1.__spread(reflectedMembers));
                    }
                });
            }
            // If this class was declared as a VariableDeclaration then it may have static properties
            // attached to the variable rather than the class itself
            // For example:
            // ```
            // let MyClass = class MyClass {
            //   // no static properties here!
            // }
            // MyClass.staticProperty = ...;
            // ```
            var variableDeclaration = getVariableDeclarationOfDeclaration(symbol.valueDeclaration);
            if (variableDeclaration !== undefined) {
                var variableSymbol = this.checker.getSymbolAtLocation(variableDeclaration.name);
                if (variableSymbol && variableSymbol.exports) {
                    variableSymbol.exports.forEach(function (value, key) {
                        var decorators = decoratorsMap.get(key);
                        var reflectedMembers = _this.reflectMembers(value, decorators, true);
                        if (reflectedMembers) {
                            decoratorsMap.delete(key);
                            members.push.apply(members, tslib_1.__spread(reflectedMembers));
                        }
                    });
                }
            }
            // Deal with any decorated properties that were not initialized in the class
            decoratorsMap.forEach(function (value, key) {
                members.push({
                    implementation: null,
                    decorators: value,
                    isStatic: false,
                    kind: reflection_1.ClassMemberKind.Property,
                    name: key,
                    nameNode: null,
                    node: null,
                    type: null,
                    value: null
                });
            });
            return members;
        };
        /**
         * Get all the member decorators for the given class.
         * @param classSymbol the class whose member decorators we are interested in.
         * @returns a map whose keys are the name of the members and whose values are collections of
         * decorators for the given member.
         */
        Esm2015ReflectionHost.prototype.getMemberDecorators = function (classSymbol) {
            var decoratorsProperty = this.getStaticProperty(classSymbol, exports.PROP_DECORATORS);
            if (decoratorsProperty) {
                return this.getMemberDecoratorsFromStaticProperty(decoratorsProperty);
            }
            else {
                return this.getMemberDecoratorsFromHelperCalls(classSymbol);
            }
        };
        /**
         * Member decorators may be declared as static properties of the class:
         *
         * ```
         * SomeDirective.propDecorators = {
         *   "ngForOf": [{ type: Input },],
         *   "ngForTrackBy": [{ type: Input },],
         *   "ngForTemplate": [{ type: Input },],
         * };
         * ```
         *
         * @param decoratorsProperty the class whose member decorators we are interested in.
         * @returns a map whose keys are the name of the members and whose values are collections of
         * decorators for the given member.
         */
        Esm2015ReflectionHost.prototype.getMemberDecoratorsFromStaticProperty = function (decoratorsProperty) {
            var _this = this;
            var memberDecorators = new Map();
            // Symbol of the identifier for `SomeDirective.propDecorators`.
            var propDecoratorsMap = getPropertyValueFromSymbol(decoratorsProperty);
            if (propDecoratorsMap && ts.isObjectLiteralExpression(propDecoratorsMap)) {
                var propertiesMap = reflection_1.reflectObjectLiteral(propDecoratorsMap);
                propertiesMap.forEach(function (value, name) {
                    var decorators = _this.reflectDecorators(value).filter(function (decorator) { return _this.isFromCore(decorator); });
                    if (decorators.length) {
                        memberDecorators.set(name, decorators);
                    }
                });
            }
            return memberDecorators;
        };
        /**
         * Member decorators may be declared via helper call statements.
         *
         * ```
         * __decorate([
         *     Input(),
         *     __metadata("design:type", String)
         * ], SomeDirective.prototype, "input1", void 0);
         * ```
         *
         * @param classSymbol the class whose member decorators we are interested in.
         * @returns a map whose keys are the name of the members and whose values are collections of
         * decorators for the given member.
         */
        Esm2015ReflectionHost.prototype.getMemberDecoratorsFromHelperCalls = function (classSymbol) {
            var _this = this;
            var memberDecoratorMap = new Map();
            var helperCalls = this.getHelperCallsForClass(classSymbol, '__decorate');
            helperCalls.forEach(function (helperCall) {
                var memberDecorators = _this.reflectDecoratorsFromHelperCall(helperCall, makeMemberTargetFilter(classSymbol.name)).memberDecorators;
                memberDecorators.forEach(function (decorators, memberName) {
                    if (memberName) {
                        var memberDecorators_1 = memberDecoratorMap.has(memberName) ? memberDecoratorMap.get(memberName) : [];
                        var coreDecorators = decorators.filter(function (decorator) { return _this.isFromCore(decorator); });
                        memberDecoratorMap.set(memberName, memberDecorators_1.concat(coreDecorators));
                    }
                });
            });
            return memberDecoratorMap;
        };
        /**
         * Extract decorator info from `__decorate` helper function calls.
         * @param helperCall the call to a helper that may contain decorator calls
         * @param targetFilter a function to filter out targets that we are not interested in.
         * @returns a mapping from member name to decorators, where the key is either the name of the
         * member or `undefined` if it refers to decorators on the class as a whole.
         */
        Esm2015ReflectionHost.prototype.reflectDecoratorsFromHelperCall = function (helperCall, targetFilter) {
            var _this = this;
            var classDecorators = [];
            var memberDecorators = new Map();
            // First check that the `target` argument is correct
            if (targetFilter(helperCall.arguments[1])) {
                // Grab the `decorators` argument which should be an array of calls
                var decoratorCalls = helperCall.arguments[0];
                if (decoratorCalls && ts.isArrayLiteralExpression(decoratorCalls)) {
                    decoratorCalls.elements.forEach(function (element) {
                        // We only care about those elements that are actual calls
                        if (ts.isCallExpression(element)) {
                            var decorator = _this.reflectDecoratorCall(element);
                            if (decorator) {
                                var keyArg = helperCall.arguments[2];
                                var keyName = keyArg && ts.isStringLiteral(keyArg) ? keyArg.text : undefined;
                                if (keyName === undefined) {
                                    classDecorators.push(decorator);
                                }
                                else {
                                    var decorators = memberDecorators.has(keyName) ? memberDecorators.get(keyName) : [];
                                    decorators.push(decorator);
                                    memberDecorators.set(keyName, decorators);
                                }
                            }
                        }
                    });
                }
            }
            return { classDecorators: classDecorators, memberDecorators: memberDecorators };
        };
        /**
         * Extract the decorator information from a call to a decorator as a function.
         * This happens when the decorators has been used in a `__decorate` helper call.
         * For example:
         *
         * ```
         * __decorate([
         *   Directive({ selector: '[someDirective]' }),
         * ], SomeDirective);
         * ```
         *
         * Here the `Directive` decorator is decorating `SomeDirective` and the options for
         * the decorator are passed as arguments to the `Directive()` call.
         *
         * @param call the call to the decorator.
         * @returns a decorator containing the reflected information, or null if the call
         * is not a valid decorator call.
         */
        Esm2015ReflectionHost.prototype.reflectDecoratorCall = function (call) {
            var decoratorExpression = call.expression;
            if (reflection_1.isDecoratorIdentifier(decoratorExpression)) {
                // We found a decorator!
                var decoratorIdentifier = ts.isIdentifier(decoratorExpression) ? decoratorExpression : decoratorExpression.name;
                return {
                    name: decoratorIdentifier.text,
                    identifier: decoratorIdentifier,
                    import: this.getImportOfIdentifier(decoratorIdentifier),
                    node: call,
                    args: Array.from(call.arguments)
                };
            }
            return null;
        };
        /**
         * Check the given statement to see if it is a call to the specified helper function or null if
         * not found.
         *
         * Matching statements will look like:  `tslib_1.__decorate(...);`.
         * @param statement the statement that may contain the call.
         * @param helperName the name of the helper we are looking for.
         * @returns the node that corresponds to the `__decorate(...)` call or null if the statement
         * does not match.
         */
        Esm2015ReflectionHost.prototype.getHelperCall = function (statement, helperName) {
            if (ts.isExpressionStatement(statement)) {
                var expression = statement.expression;
                while (isAssignment(expression)) {
                    expression = expression.right;
                }
                if (ts.isCallExpression(expression) && getCalleeName(expression) === helperName) {
                    return expression;
                }
            }
            return null;
        };
        /**
         * Reflect over the given array node and extract decorator information from each element.
         *
         * This is used for decorators that are defined in static properties. For example:
         *
         * ```
         * SomeDirective.decorators = [
         *   { type: Directive, args: [{ selector: '[someDirective]' },] }
         * ];
         * ```
         *
         * @param decoratorsArray an expression that contains decorator information.
         * @returns an array of decorator info that was reflected from the array node.
         */
        Esm2015ReflectionHost.prototype.reflectDecorators = function (decoratorsArray) {
            var _this = this;
            var decorators = [];
            if (ts.isArrayLiteralExpression(decoratorsArray)) {
                // Add each decorator that is imported from `@angular/core` into the `decorators` array
                decoratorsArray.elements.forEach(function (node) {
                    // If the decorator is not an object literal expression then we are not interested
                    if (ts.isObjectLiteralExpression(node)) {
                        // We are only interested in objects of the form: `{ type: DecoratorType, args: [...] }`
                        var decorator = reflection_1.reflectObjectLiteral(node);
                        // Is the value of the `type` property an identifier?
                        if (decorator.has('type')) {
                            var decoratorType = decorator.get('type');
                            if (reflection_1.isDecoratorIdentifier(decoratorType)) {
                                var decoratorIdentifier = ts.isIdentifier(decoratorType) ? decoratorType : decoratorType.name;
                                decorators.push({
                                    name: decoratorIdentifier.text,
                                    identifier: decoratorType,
                                    import: _this.getImportOfIdentifier(decoratorIdentifier), node: node,
                                    args: getDecoratorArgs(node),
                                });
                            }
                        }
                    }
                });
            }
            return decorators;
        };
        /**
         * Reflect over a symbol and extract the member information, combining it with the
         * provided decorator information, and whether it is a static member.
         *
         * A single symbol may represent multiple class members in the case of accessors;
         * an equally named getter/setter accessor pair is combined into a single symbol.
         * When the symbol is recognized as representing an accessor, its declarations are
         * analyzed such that both the setter and getter accessor are returned as separate
         * class members.
         *
         * One difference wrt the TypeScript host is that in ES2015, we cannot see which
         * accessor originally had any decorators applied to them, as decorators are applied
         * to the property descriptor in general, not a specific accessor. If an accessor
         * has both a setter and getter, any decorators are only attached to the setter member.
         *
         * @param symbol the symbol for the member to reflect over.
         * @param decorators an array of decorators associated with the member.
         * @param isStatic true if this member is static, false if it is an instance property.
         * @returns the reflected member information, or null if the symbol is not a member.
         */
        Esm2015ReflectionHost.prototype.reflectMembers = function (symbol, decorators, isStatic) {
            if (symbol.flags & ts.SymbolFlags.Accessor) {
                var members = [];
                var setter = symbol.declarations && symbol.declarations.find(ts.isSetAccessor);
                var getter = symbol.declarations && symbol.declarations.find(ts.isGetAccessor);
                var setterMember = setter && this.reflectMember(setter, reflection_1.ClassMemberKind.Setter, decorators, isStatic);
                if (setterMember) {
                    members.push(setterMember);
                    // Prevent attaching the decorators to a potential getter. In ES2015, we can't tell where
                    // the decorators were originally attached to, however we only want to attach them to a
                    // single `ClassMember` as otherwise ngtsc would handle the same decorators twice.
                    decorators = undefined;
                }
                var getterMember = getter && this.reflectMember(getter, reflection_1.ClassMemberKind.Getter, decorators, isStatic);
                if (getterMember) {
                    members.push(getterMember);
                }
                return members;
            }
            var kind = null;
            if (symbol.flags & ts.SymbolFlags.Method) {
                kind = reflection_1.ClassMemberKind.Method;
            }
            else if (symbol.flags & ts.SymbolFlags.Property) {
                kind = reflection_1.ClassMemberKind.Property;
            }
            var node = symbol.valueDeclaration || symbol.declarations && symbol.declarations[0];
            if (!node) {
                // If the symbol has been imported from a TypeScript typings file then the compiler
                // may pass the `prototype` symbol as an export of the class.
                // But this has no declaration. In this case we just quietly ignore it.
                return null;
            }
            var member = this.reflectMember(node, kind, decorators, isStatic);
            if (!member) {
                return null;
            }
            return [member];
        };
        /**
         * Reflect over a symbol and extract the member information, combining it with the
         * provided decorator information, and whether it is a static member.
         * @param node the declaration node for the member to reflect over.
         * @param kind the assumed kind of the member, may become more accurate during reflection.
         * @param decorators an array of decorators associated with the member.
         * @param isStatic true if this member is static, false if it is an instance property.
         * @returns the reflected member information, or null if the symbol is not a member.
         */
        Esm2015ReflectionHost.prototype.reflectMember = function (node, kind, decorators, isStatic) {
            var value = null;
            var name = null;
            var nameNode = null;
            if (!isClassMemberType(node)) {
                return null;
            }
            if (isStatic && isPropertyAccess(node)) {
                name = node.name.text;
                value = kind === reflection_1.ClassMemberKind.Property ? node.parent.right : null;
            }
            else if (isThisAssignment(node)) {
                kind = reflection_1.ClassMemberKind.Property;
                name = node.left.name.text;
                value = node.right;
                isStatic = false;
            }
            else if (ts.isConstructorDeclaration(node)) {
                kind = reflection_1.ClassMemberKind.Constructor;
                name = 'constructor';
                isStatic = false;
            }
            if (kind === null) {
                this.logger.warn("Unknown member type: \"" + node.getText());
                return null;
            }
            if (!name) {
                if (isNamedDeclaration(node)) {
                    name = node.name.text;
                    nameNode = node.name;
                }
                else {
                    return null;
                }
            }
            // If we have still not determined if this is a static or instance member then
            // look for the `static` keyword on the declaration
            if (isStatic === undefined) {
                isStatic = node.modifiers !== undefined &&
                    node.modifiers.some(function (mod) { return mod.kind === ts.SyntaxKind.StaticKeyword; });
            }
            var type = node.type || null;
            return {
                node: node,
                implementation: node, kind: kind, type: type, name: name, nameNode: nameNode, value: value, isStatic: isStatic,
                decorators: decorators || []
            };
        };
        /**
         * Find the declarations of the constructor parameters of a class identified by its symbol.
         * @param classSymbol the class whose parameters we want to find.
         * @returns an array of `ts.ParameterDeclaration` objects representing each of the parameters in
         * the class's constructor or null if there is no constructor.
         */
        Esm2015ReflectionHost.prototype.getConstructorParameterDeclarations = function (classSymbol) {
            if (classSymbol.members && classSymbol.members.has(exports.CONSTRUCTOR)) {
                var constructorSymbol = classSymbol.members.get(exports.CONSTRUCTOR);
                // For some reason the constructor does not have a `valueDeclaration` ?!?
                var constructor = constructorSymbol.declarations &&
                    constructorSymbol.declarations[0];
                if (!constructor) {
                    return [];
                }
                if (constructor.parameters.length > 0) {
                    return Array.from(constructor.parameters);
                }
                if (isSynthesizedConstructor(constructor)) {
                    return null;
                }
                return [];
            }
            return null;
        };
        /**
         * Get the parameter decorators of a class constructor.
         *
         * @param classSymbol the class whose parameter info we want to get.
         * @param parameterNodes the array of TypeScript parameter nodes for this class's constructor.
         * @returns an array of constructor parameter info objects.
         */
        Esm2015ReflectionHost.prototype.getConstructorParamInfo = function (classSymbol, parameterNodes) {
            var paramsProperty = this.getStaticProperty(classSymbol, exports.CONSTRUCTOR_PARAMS);
            var paramInfo = paramsProperty ?
                this.getParamInfoFromStaticProperty(paramsProperty) :
                this.getParamInfoFromHelperCall(classSymbol, parameterNodes);
            return parameterNodes.map(function (node, index) {
                var _a = paramInfo && paramInfo[index] ?
                    paramInfo[index] :
                    { decorators: null, typeExpression: null }, decorators = _a.decorators, typeExpression = _a.typeExpression;
                var nameNode = node.name;
                return {
                    name: utils_1.getNameText(nameNode),
                    nameNode: nameNode,
                    typeValueReference: typeExpression !== null ?
                        { local: true, expression: typeExpression, defaultImportStatement: null } :
                        null,
                    typeNode: null, decorators: decorators
                };
            });
        };
        /**
         * Get the parameter type and decorators for the constructor of a class,
         * where the information is stored on a static property of the class.
         *
         * Note that in ESM2015, the property is defined an array, or by an arrow function that returns an
         * array, of decorator and type information.
         *
         * For example,
         *
         * ```
         * SomeDirective.ctorParameters = () => [
         *   {type: ViewContainerRef},
         *   {type: TemplateRef},
         *   {type: undefined, decorators: [{ type: Inject, args: [INJECTED_TOKEN]}]},
         * ];
         * ```
         *
         * or
         *
         * ```
         * SomeDirective.ctorParameters = [
         *   {type: ViewContainerRef},
         *   {type: TemplateRef},
         *   {type: undefined, decorators: [{type: Inject, args: [INJECTED_TOKEN]}]},
         * ];
         * ```
         *
         * @param paramDecoratorsProperty the property that holds the parameter info we want to get.
         * @returns an array of objects containing the type and decorators for each parameter.
         */
        Esm2015ReflectionHost.prototype.getParamInfoFromStaticProperty = function (paramDecoratorsProperty) {
            var _this = this;
            var paramDecorators = getPropertyValueFromSymbol(paramDecoratorsProperty);
            if (paramDecorators) {
                // The decorators array may be wrapped in an arrow function. If so unwrap it.
                var container = ts.isArrowFunction(paramDecorators) ? paramDecorators.body : paramDecorators;
                if (ts.isArrayLiteralExpression(container)) {
                    var elements = container.elements;
                    return elements
                        .map(function (element) {
                        return ts.isObjectLiteralExpression(element) ? reflection_1.reflectObjectLiteral(element) : null;
                    })
                        .map(function (paramInfo) {
                        var typeExpression = paramInfo && paramInfo.has('type') ? paramInfo.get('type') : null;
                        var decoratorInfo = paramInfo && paramInfo.has('decorators') ? paramInfo.get('decorators') : null;
                        var decorators = decoratorInfo &&
                            _this.reflectDecorators(decoratorInfo)
                                .filter(function (decorator) { return _this.isFromCore(decorator); });
                        return { typeExpression: typeExpression, decorators: decorators };
                    });
                }
                else if (paramDecorators !== undefined) {
                    this.logger.warn('Invalid constructor parameter decorator in ' +
                        paramDecorators.getSourceFile().fileName + ':\n', paramDecorators.getText());
                }
            }
            return null;
        };
        /**
         * Get the parameter type and decorators for a class where the information is stored via
         * calls to `__decorate` helpers.
         *
         * Reflect over the helpers to find the decorators and types about each of
         * the class's constructor parameters.
         *
         * @param classSymbol the class whose parameter info we want to get.
         * @param parameterNodes the array of TypeScript parameter nodes for this class's constructor.
         * @returns an array of objects containing the type and decorators for each parameter.
         */
        Esm2015ReflectionHost.prototype.getParamInfoFromHelperCall = function (classSymbol, parameterNodes) {
            var _this = this;
            var parameters = parameterNodes.map(function () { return ({ typeExpression: null, decorators: null }); });
            var helperCalls = this.getHelperCallsForClass(classSymbol, '__decorate');
            helperCalls.forEach(function (helperCall) {
                var classDecorators = _this.reflectDecoratorsFromHelperCall(helperCall, makeClassTargetFilter(classSymbol.name)).classDecorators;
                classDecorators.forEach(function (call) {
                    switch (call.name) {
                        case '__metadata':
                            var metadataArg = call.args && call.args[0];
                            var typesArg = call.args && call.args[1];
                            var isParamTypeDecorator = metadataArg && ts.isStringLiteral(metadataArg) &&
                                metadataArg.text === 'design:paramtypes';
                            var types = typesArg && ts.isArrayLiteralExpression(typesArg) && typesArg.elements;
                            if (isParamTypeDecorator && types) {
                                types.forEach(function (type, index) { return parameters[index].typeExpression = type; });
                            }
                            break;
                        case '__param':
                            var paramIndexArg = call.args && call.args[0];
                            var decoratorCallArg = call.args && call.args[1];
                            var paramIndex = paramIndexArg && ts.isNumericLiteral(paramIndexArg) ?
                                parseInt(paramIndexArg.text, 10) :
                                NaN;
                            var decorator = decoratorCallArg && ts.isCallExpression(decoratorCallArg) ?
                                _this.reflectDecoratorCall(decoratorCallArg) :
                                null;
                            if (!isNaN(paramIndex) && decorator) {
                                var decorators = parameters[paramIndex].decorators =
                                    parameters[paramIndex].decorators || [];
                                decorators.push(decorator);
                            }
                            break;
                    }
                });
            });
            return parameters;
        };
        /**
         * Search statements related to the given class for calls to the specified helper.
         * @param classSymbol the class whose helper calls we are interested in.
         * @param helperName the name of the helper (e.g. `__decorate`) whose calls we are interested
         * in.
         * @returns an array of CallExpression nodes for each matching helper call.
         */
        Esm2015ReflectionHost.prototype.getHelperCallsForClass = function (classSymbol, helperName) {
            var _this = this;
            return this.getStatementsForClass(classSymbol)
                .map(function (statement) { return _this.getHelperCall(statement, helperName); })
                .filter(utils_1.isDefined);
        };
        /**
         * Find statements related to the given class that may contain calls to a helper.
         *
         * In ESM2015 code the helper calls are in the top level module, so we have to consider
         * all the statements in the module.
         *
         * @param classSymbol the class whose helper calls we are interested in.
         * @returns an array of statements that may contain helper calls.
         */
        Esm2015ReflectionHost.prototype.getStatementsForClass = function (classSymbol) {
            return Array.from(classSymbol.valueDeclaration.getSourceFile().statements);
        };
        /**
         * Test whether a decorator was imported from `@angular/core`.
         *
         * Is the decorator:
         * * externally imported from `@angular/core`?
         * * the current hosted program is actually `@angular/core` and
         *   - relatively internally imported; or
         *   - not imported, from the current file.
         *
         * @param decorator the decorator to test.
         */
        Esm2015ReflectionHost.prototype.isFromCore = function (decorator) {
            if (this.isCore) {
                return !decorator.import || /^\./.test(decorator.import.from);
            }
            else {
                return !!decorator.import && decorator.import.from === '@angular/core';
            }
        };
        /**
         * Extract all the class declarations from the dtsTypings program, storing them in a map
         * where the key is the declared name of the class and the value is the declaration itself.
         *
         * It is possible for there to be multiple class declarations with the same local name.
         * Only the first declaration with a given name is added to the map; subsequent classes will be
         * ignored.
         *
         * We are most interested in classes that are publicly exported from the entry point, so these
         * are added to the map first, to ensure that they are not ignored.
         *
         * @param dtsRootFileName The filename of the entry-point to the `dtsTypings` program.
         * @param dtsProgram The program containing all the typings files.
         * @returns a map of class names to class declarations.
         */
        Esm2015ReflectionHost.prototype.computeDtsDeclarationMap = function (dtsRootFileName, dtsProgram) {
            var dtsDeclarationMap = new Map();
            var checker = dtsProgram.getTypeChecker();
            // First add all the classes that are publicly exported from the entry-point
            var rootFile = dtsProgram.getSourceFile(dtsRootFileName);
            if (!rootFile) {
                throw new Error("The given file " + dtsRootFileName + " is not part of the typings program.");
            }
            collectExportedDeclarations(checker, dtsDeclarationMap, rootFile);
            // Now add any additional classes that are exported from individual  dts files,
            // but are not publicly exported from the entry-point.
            dtsProgram.getSourceFiles().forEach(function (sourceFile) { collectExportedDeclarations(checker, dtsDeclarationMap, sourceFile); });
            return dtsDeclarationMap;
        };
        /**
         * Parse a function/method node (or its implementation), to see if it returns a
         * `ModuleWithProviders` object.
         * @param name The name of the function.
         * @param node the node to check - this could be a function, a method or a variable declaration.
         * @param implementation the actual function expression if `node` is a variable declaration.
         * @param container the class that contains the function, if it is a method.
         * @returns info about the function if it does return a `ModuleWithProviders` object; `null`
         * otherwise.
         */
        Esm2015ReflectionHost.prototype.parseForModuleWithProviders = function (name, node, implementation, container) {
            if (implementation === void 0) { implementation = node; }
            if (container === void 0) { container = null; }
            if (implementation === null ||
                (!ts.isFunctionDeclaration(implementation) && !ts.isMethodDeclaration(implementation) &&
                    !ts.isFunctionExpression(implementation))) {
                return null;
            }
            var declaration = implementation;
            var definition = this.getDefinitionOfFunction(declaration);
            if (definition === null) {
                return null;
            }
            var body = definition.body;
            var lastStatement = body && body[body.length - 1];
            var returnExpression = lastStatement && ts.isReturnStatement(lastStatement) && lastStatement.expression || null;
            var ngModuleProperty = returnExpression && ts.isObjectLiteralExpression(returnExpression) &&
                returnExpression.properties.find(function (prop) {
                    return !!prop.name && ts.isIdentifier(prop.name) && prop.name.text === 'ngModule';
                }) ||
                null;
            if (!ngModuleProperty || !ts.isPropertyAssignment(ngModuleProperty)) {
                return null;
            }
            // The ngModuleValue could be of the form `SomeModule` or `namespace_1.SomeModule`
            var ngModuleValue = ngModuleProperty.initializer;
            if (!ts.isIdentifier(ngModuleValue) && !ts.isPropertyAccessExpression(ngModuleValue)) {
                return null;
            }
            var ngModuleDeclaration = this.getDeclarationOfExpression(ngModuleValue);
            if (!ngModuleDeclaration) {
                throw new Error("Cannot find a declaration for NgModule " + ngModuleValue.getText() + " referenced in \"" + declaration.getText() + "\"");
            }
            if (!utils_1.hasNameIdentifier(ngModuleDeclaration.node)) {
                return null;
            }
            return {
                name: name,
                ngModule: ngModuleDeclaration, declaration: declaration, container: container
            };
        };
        Esm2015ReflectionHost.prototype.getDeclarationOfExpression = function (expression) {
            if (ts.isIdentifier(expression)) {
                return this.getDeclarationOfIdentifier(expression);
            }
            if (!ts.isPropertyAccessExpression(expression) || !ts.isIdentifier(expression.expression)) {
                return null;
            }
            var namespaceDecl = this.getDeclarationOfIdentifier(expression.expression);
            if (!namespaceDecl || !ts.isSourceFile(namespaceDecl.node)) {
                return null;
            }
            var namespaceExports = this.getExportsOfModule(namespaceDecl.node);
            if (namespaceExports === null) {
                return null;
            }
            if (!namespaceExports.has(expression.name.text)) {
                return null;
            }
            var exportDecl = namespaceExports.get(expression.name.text);
            return tslib_1.__assign({}, exportDecl, { viaModule: namespaceDecl.viaModule });
        };
        return Esm2015ReflectionHost;
    }(reflection_1.TypeScriptReflectionHost));
    exports.Esm2015ReflectionHost = Esm2015ReflectionHost;
    /**
     * Test whether a statement node is an assignment statement.
     * @param statement the statement to test.
     */
    function isAssignmentStatement(statement) {
        return ts.isExpressionStatement(statement) && isAssignment(statement.expression) &&
            ts.isIdentifier(statement.expression.left);
    }
    exports.isAssignmentStatement = isAssignmentStatement;
    function isAssignment(node) {
        return ts.isBinaryExpression(node) && node.operatorToken.kind === ts.SyntaxKind.EqualsToken;
    }
    exports.isAssignment = isAssignment;
    /**
     * Creates a function that tests whether the given expression is a class target.
     * @param className the name of the class we want to target.
     */
    function makeClassTargetFilter(className) {
        return function (target) { return ts.isIdentifier(target) && target.text === className; };
    }
    exports.makeClassTargetFilter = makeClassTargetFilter;
    /**
     * Creates a function that tests whether the given expression is a class member target.
     * @param className the name of the class we want to target.
     */
    function makeMemberTargetFilter(className) {
        return function (target) { return ts.isPropertyAccessExpression(target) &&
            ts.isIdentifier(target.expression) && target.expression.text === className &&
            target.name.text === 'prototype'; };
    }
    exports.makeMemberTargetFilter = makeMemberTargetFilter;
    /**
     * Helper method to extract the value of a property given the property's "symbol",
     * which is actually the symbol of the identifier of the property.
     */
    function getPropertyValueFromSymbol(propSymbol) {
        var propIdentifier = propSymbol.valueDeclaration;
        var parent = propIdentifier && propIdentifier.parent;
        return parent && ts.isBinaryExpression(parent) ? parent.right : undefined;
    }
    exports.getPropertyValueFromSymbol = getPropertyValueFromSymbol;
    /**
     * A callee could be one of: `__decorate(...)` or `tslib_1.__decorate`.
     */
    function getCalleeName(call) {
        if (ts.isIdentifier(call.expression)) {
            return call.expression.text;
        }
        if (ts.isPropertyAccessExpression(call.expression)) {
            return call.expression.name.text;
        }
        return null;
    }
    ///////////// Internal Helpers /////////////
    /**
     * In ES2015, a class may be declared using a variable declaration of the following structure:
     *
     * ```
     * var MyClass = MyClass_1 = class MyClass {};
     * ```
     *
     * Here, the intermediate `MyClass_1` assignment is optional. In the above example, the
     * `class MyClass {}` expression is returned as declaration of `MyClass`. Note that if `node`
     * represents a regular class declaration, it will be returned as-is.
     *
     * @param node the node that represents the class whose declaration we are finding.
     * @returns the declaration of the class or `null` if it is not a "class".
     */
    function getInnerClassDeclaration(node) {
        // Recognize a variable declaration of the form `var MyClass = class MyClass {}` or
        // `var MyClass = MyClass_1 = class MyClass {};`
        if (ts.isVariableDeclaration(node) && node.initializer !== undefined) {
            node = node.initializer;
            while (isAssignment(node)) {
                node = node.right;
            }
        }
        if (!ts.isClassDeclaration(node) && !ts.isClassExpression(node)) {
            return null;
        }
        return utils_1.hasNameIdentifier(node) ? node : null;
    }
    function getDecoratorArgs(node) {
        // The arguments of a decorator are held in the `args` property of its declaration object.
        var argsProperty = node.properties.filter(ts.isPropertyAssignment)
            .find(function (property) { return utils_1.getNameText(property.name) === 'args'; });
        var argsExpression = argsProperty && argsProperty.initializer;
        return argsExpression && ts.isArrayLiteralExpression(argsExpression) ?
            Array.from(argsExpression.elements) :
            [];
    }
    function isPropertyAccess(node) {
        return !!node.parent && ts.isBinaryExpression(node.parent) && ts.isPropertyAccessExpression(node);
    }
    function isThisAssignment(node) {
        return ts.isBinaryExpression(node) && ts.isPropertyAccessExpression(node.left) &&
            node.left.expression.kind === ts.SyntaxKind.ThisKeyword;
    }
    function isNamedDeclaration(node) {
        var anyNode = node;
        return !!anyNode.name && ts.isIdentifier(anyNode.name);
    }
    function isClassMemberType(node) {
        return ts.isClassElement(node) || isPropertyAccess(node) || ts.isBinaryExpression(node);
    }
    /**
     * Collect mappings between exported declarations in a source file and its associated
     * declaration in the typings program.
     */
    function collectExportedDeclarations(checker, dtsDeclarationMap, srcFile) {
        var srcModule = srcFile && checker.getSymbolAtLocation(srcFile);
        var moduleExports = srcModule && checker.getExportsOfModule(srcModule);
        if (moduleExports) {
            moduleExports.forEach(function (exportedSymbol) {
                if (exportedSymbol.flags & ts.SymbolFlags.Alias) {
                    exportedSymbol = checker.getAliasedSymbol(exportedSymbol);
                }
                var declaration = exportedSymbol.valueDeclaration;
                var name = exportedSymbol.name;
                if (declaration && !dtsDeclarationMap.has(name)) {
                    dtsDeclarationMap.set(name, declaration);
                }
            });
        }
    }
    /**
     * Attempt to resolve the variable declaration that the given declaration is assigned to.
     * For example, for the following code:
     *
     * ```
     * var MyClass = MyClass_1 = class MyClass {};
     * ```
     *
     * and the provided declaration being `class MyClass {}`, this will return the `var MyClass`
     * declaration.
     *
     * @param declaration The declaration for which any variable declaration should be obtained.
     * @returns the outer variable declaration if found, undefined otherwise.
     */
    function getVariableDeclarationOfDeclaration(declaration) {
        var node = declaration.parent;
        // Detect an intermediary variable assignment and skip over it.
        if (isAssignment(node) && ts.isIdentifier(node.left)) {
            node = node.parent;
        }
        return ts.isVariableDeclaration(node) ? node : undefined;
    }
    /**
     * A constructor function may have been "synthesized" by TypeScript during JavaScript emit,
     * in the case no user-defined constructor exists and e.g. property initializers are used.
     * Those initializers need to be emitted into a constructor in JavaScript, so the TypeScript
     * compiler generates a synthetic constructor.
     *
     * We need to identify such constructors as ngcc needs to be able to tell if a class did
     * originally have a constructor in the TypeScript source. When a class has a superclass,
     * a synthesized constructor must not be considered as a user-defined constructor as that
     * prevents a base factory call from being created by ngtsc, resulting in a factory function
     * that does not inject the dependencies of the superclass. Hence, we identify a default
     * synthesized super call in the constructor body, according to the structure that TypeScript
     * emits during JavaScript emit:
     * https://github.com/Microsoft/TypeScript/blob/v3.2.2/src/compiler/transformers/ts.ts#L1068-L1082
     *
     * @param constructor a constructor function to test
     * @returns true if the constructor appears to have been synthesized
     */
    function isSynthesizedConstructor(constructor) {
        if (!constructor.body)
            return false;
        var firstStatement = constructor.body.statements[0];
        if (!firstStatement || !ts.isExpressionStatement(firstStatement))
            return false;
        return isSynthesizedSuperCall(firstStatement.expression);
    }
    /**
     * Tests whether the expression appears to have been synthesized by TypeScript, i.e. whether
     * it is of the following form:
     *
     * ```
     * super(...arguments);
     * ```
     *
     * @param expression the expression that is to be tested
     * @returns true if the expression appears to be a synthesized super call
     */
    function isSynthesizedSuperCall(expression) {
        if (!ts.isCallExpression(expression))
            return false;
        if (expression.expression.kind !== ts.SyntaxKind.SuperKeyword)
            return false;
        if (expression.arguments.length !== 1)
            return false;
        var argument = expression.arguments[0];
        return ts.isSpreadElement(argument) && ts.isIdentifier(argument.expression) &&
            argument.expression.text === 'arguments';
    }
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZXNtMjAxNV9ob3N0LmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vLi4vLi4vLi4vLi4vcGFja2FnZXMvY29tcGlsZXItY2xpL25nY2Mvc3JjL2hvc3QvZXNtMjAxNV9ob3N0LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBOzs7Ozs7R0FNRzs7Ozs7Ozs7Ozs7OztJQUVILCtCQUFpQztJQUdqQyx5RUFBZ087SUFHaE8sOERBQTRFO0lBRTVFLDJFQUEySjtJQUU5SSxRQUFBLFVBQVUsR0FBRyxZQUEyQixDQUFDO0lBQ3pDLFFBQUEsZUFBZSxHQUFHLGdCQUErQixDQUFDO0lBQ2xELFFBQUEsV0FBVyxHQUFHLGVBQThCLENBQUM7SUFDN0MsUUFBQSxrQkFBa0IsR0FBRyxnQkFBK0IsQ0FBQztJQUVsRTs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7T0EwQkc7SUFDSDtRQUEyQyxpREFBd0I7UUF5QmpFLCtCQUNjLE1BQWMsRUFBWSxNQUFlLEVBQUUsT0FBdUIsRUFDNUUsR0FBd0I7WUFGNUIsWUFHRSxrQkFBTSxPQUFPLENBQUMsU0FFZjtZQUphLFlBQU0sR0FBTixNQUFNLENBQVE7WUFBWSxZQUFNLEdBQU4sTUFBTSxDQUFTO1lBdkJ2RDs7ZUFFRztZQUNPLDZCQUF1QixHQUFHLElBQUksR0FBRyxFQUFpQixDQUFDO1lBRTdEOzs7Ozs7Ozs7Ozs7OztlQWNHO1lBQ08sOEJBQXdCLEdBQUcsSUFBSSxHQUFHLEVBQWlDLENBQUM7WUFNNUUsS0FBSSxDQUFDLGlCQUFpQixHQUFHLEdBQUcsSUFBSSxLQUFJLENBQUMsd0JBQXdCLENBQUMsR0FBRyxDQUFDLElBQUksRUFBRSxHQUFHLENBQUMsT0FBTyxDQUFDLElBQUksSUFBSSxDQUFDOztRQUMvRixDQUFDO1FBRUQ7Ozs7Ozs7Ozs7Ozs7Ozs7V0FnQkc7UUFDSCxtREFBbUIsR0FBbkIsVUFBb0IsSUFBYTtZQUMvQixPQUFPLHdCQUF3QixDQUFDLElBQUksQ0FBQyxJQUFJLFNB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QyxZQUFZO1lBQUUsT0FBTyxLQUFLLENBQUM7UUFDNUUsSUFBSSxVQUFVLENBQUMsU0FBUyxDQUFDLE1BQU0sS0FBSyxDQUFDO1lBQUUsT0FBTyxLQUFLLENBQUM7UUFFcEQsSUFBTSxRQUFRLEdBQUcsVUFBVSxDQUFDLFNBQVMsQ0FBQyxDQUFDLENBQUMsQ0FBQztRQUN6QyxPQUFPLEVBQUUsQ0FBQyxlQUFlLENBQUMsUUFBUSxDQUFDLElBQUksRUFBRSxDQUFDLFlBQVksQ0FBQyxRQUFRLENBQUMsVUFBVSxDQUFDO1lBQ3ZFLFFBQVEsQ0FBQyxVQUFVLENBQUMsSUFBSSxLQUFLLFdBQVcsQ0FBQztJQUMvQyxDQUFDIiwic291cmNlc0NvbnRlbnQiOlsiLyoqXG4gKiBAbGljZW5zZVxuICogQ29weXJpZ2h0IEdvb2dsZSBJbmMuIEFsbCBSaWdodHMgUmVzZXJ2ZWQuXG4gKlxuICogVXNlIG9mIHRoaXMgc291cmNlIGNvZGUgaXMgZ292ZXJuZWQgYnkgYW4gTUlULXN0eWxlIGxpY2Vuc2UgdGhhdCBjYW4gYmVcbiAqIGZvdW5kIGluIHRoZSBMSUNFTlNFIGZpbGUgYXQgaHR0cHM6Ly9hbmd1bGFyLmlvL2xpY2Vuc2VcbiAqL1xuXG5pbXBvcnQgKiBhcyB0cyBmcm9tICd0eXBlc2NyaXB0JztcblxuaW1wb3J0IHtBYnNvbHV0ZUZzUGF0aH0gZnJvbSAnLi4vLi4vLi4vc3JjL25ndHNjL2ZpbGVfc3lzdGVtJztcbmltcG9ydCB7Q2xhc3NEZWNsYXJhdGlvbiwgQ2xhc3NNZW1iZXIsIENsYXNzTWVtYmVyS2luZCwgQ2xhc3NTeW1ib2wsIEN0b3JQYXJhbWV0ZXIsIERlY2xhcmF0aW9uLCBEZWNvcmF0b3IsIEltcG9ydCwgVHlwZVNjcmlwdFJlZmxlY3Rpb25Ib3N0LCBpc0RlY29yYXRvcklkZW50aWZpZXIsIHJlZmxlY3RPYmplY3RMaXRlcmFsfSBmcm9tICcuLi8uLi8uLi9zcmMvbmd0c2MvcmVmbGVjdGlvbic7XG5pbXBvcnQge0xvZ2dlcn0gZnJvbSAnLi4vbG9nZ2luZy9sb2dnZXInO1xuaW1wb3J0IHtCdW5kbGVQcm9ncmFtfSBmcm9tICcuLi9wYWNrYWdlcy9idW5kbGVfcHJvZ3JhbSc7XG5pbXBvcnQge2ZpbmRBbGwsIGdldE5hbWVUZXh0LCBoYXNOYW1lSWRlbnRpZmllciwgaXNEZWZpbmVkfSBmcm9tICcuLi91dGlscyc7XG5cbmltcG9ydCB7TW9kdWxlV2l0aFByb3ZpZGVyc0Z1bmN0aW9uLCBOZ2NjUmVmbGVjdGlvbkhvc3QsIFBSRV9SM19NQVJLRVIsIFN3aXRjaGFibGVWYXJpYWJsZURlY2xhcmF0aW9uLCBpc1N3aXRjaGFibGVWYXJpYWJsZURlY2xhcmF0aW9ufSBmcm9tICcuL25nY2NfaG9zdCc7XG5cbmV4cG9ydCBjb25zdCBERUNPUkFUT1JTID0gJ2RlY29yYXRvcnMnIGFzIHRzLl9fU3RyaW5nO1xuZXhwb3J0IGNvbnN0IFBST1BfREVDT1JBVE9SUyA9ICdwcm9wRGVjb3JhdG9ycycgYXMgdHMuX19TdHJpbmc7XG5leHBvcnQgY29uc3QgQ09OU1RSVUNUT1IgPSAnX19jb25zdHJ1Y3RvcicgYXMgdHMuX19TdHJpbmc7XG5leHBvcnQgY29uc3QgQ09OU1RSVUNUT1JfUEFSQU1TID0gJ2N0b3JQYXJhbWV0ZXJzJyBhcyB0cy5fX1N0cmluZztcblxuLyoqXG4gKiBFc20yMDE1IHBhY2thZ2VzIGNvbnRhaW4gRUNNQVNjcmlwdCAyMDE1IGNsYXNzZXMsIGV0Yy5cbiAqIERlY29yYXRvcnMgYXJlIGRlZmluZWQgdmlhIHN0YXRpYyBwcm9wZXJ0aWVzIG9uIHRoZSBjbGFzcy4gRm9yIGV4YW1wbGU6XG4gKlxuICogYGBgXG4gKiBjbGFzcyBTb21lRGlyZWN0aXZlIHtcbiAqIH1cbiAqIFNvbWVEaXJlY3RpdmUuZGVjb3JhdG9ycyA9IFtcbiAqICAgeyB0eXBlOiBEaXJlY3RpdmUsIGFyZ3M6IFt7IHNlbGVjdG9yOiAnW3NvbWVEaXJlY3RpdmVdJyB9LF0gfVxuICogXTtcbiAqIFNvbWVEaXJlY3RpdmUuY3RvclBhcmFtZXRlcnMgPSAoKSA9PiBbXG4gKiAgIHsgdHlwZTogVmlld0NvbnRhaW5lclJlZiwgfSxcbiAqICAgeyB0eXBlOiBUZW1wbGF0ZVJlZiwgfSxcbiAqICAgeyB0eXBlOiB1bmRlZmluZWQsIGRlY29yYXRvcnM6IFt7IHR5cGU6IEluamVjdCwgYXJnczogW0lOSkVDVEVEX1RPS0VOLF0gfSxdIH0sXG4gKiBdO1xuICogU29tZURpcmVjdGl2ZS5wcm9wRGVjb3JhdG9ycyA9IHtcbiAqICAgXCJpbnB1dDFcIjogW3sgdHlwZTogSW5wdXQgfSxdLFxuICogICBcImlucHV0MlwiOiBbeyB0eXBlOiBJbnB1dCB9LF0sXG4gKiB9O1xuICogYGBgXG4gKlxuICogKiBDbGFzc2VzIGFyZSBkZWNvcmF0ZWQgaWYgdGhleSBoYXZlIGEgc3RhdGljIHByb3BlcnR5IGNhbGxlZCBgZGVjb3JhdG9yc2AuXG4gKiAqIE1lbWJlcnMgYXJlIGRlY29yYXRlZCBpZiB0aGVyZSBpcyBhIG1hdGNoaW5nIGtleSBvbiBhIHN0YXRpYyBwcm9wZXJ0eVxuICogICBjYWxsZWQgYHByb3BEZWNvcmF0b3JzYC5cbiAqICogQ29uc3RydWN0b3IgcGFyYW1ldGVycyBkZWNvcmF0b3JzIGFyZSBmb3VuZCBvbiBhbiBvYmplY3QgcmV0dXJuZWQgZnJvbVxuICogICBhIHN0YXRpYyBtZXRob2QgY2FsbGVkIGBjdG9yUGFyYW1ldGVyc2AuXG4gKi9cbmV4cG9ydCBjbGFzcyBFc20yMDE1UmVmbGVjdGlvbkhvc3QgZXh0ZW5kcyBUeXBlU2NyaXB0UmVmbGVjdGlvbkhvc3QgaW1wbGVtZW50cyBOZ2NjUmVmbGVjdGlvbkhvc3Qge1xuICBwcm90ZWN0ZWQgZHRzRGVjbGFyYXRpb25NYXA6IE1hcDxzdHJpbmcsIHRzLkRlY2xhcmF0aW9uPnxudWxsO1xuXG4gIC8qKlxuICAgKiBUaGUgc2V0IG9mIHNvdXJjZSBmaWxlcyB0aGF0IGhhdmUgYWxyZWFkeSBiZWVuIHByZXByb2Nlc3NlZC5cbiAgICovXG4gIHByb3RlY3RlZCBwcmVwcm9jZXNzZWRTb3VyY2VGaWxlcyA9IG5ldyBTZXQ8dHMuU291cmNlRmlsZT4oKTtcblxuICAvKipcbiAgICogSW4gRVMyMDE1LCBjbGFzcyBkZWNsYXJhdGlvbnMgbWF5IGhhdmUgYmVlbiBkb3duLWxldmVsZWQgaW50byB2YXJpYWJsZSBkZWNsYXJhdGlvbnMsXG4gICAqIGluaXRpYWxpemVkIHVzaW5nIGEgY2xhc3MgZXhwcmVzc2lvbi4gSW4gY2VydGFpbiBzY2VuYXJpb3MsIGFuIGFkZGl0aW9uYWwgdmFyaWFibGVcbiAgICogaXMgaW50cm9kdWNlZCB0aGF0IHJlcHJlc2VudHMgdGhlIGNsYXNzIHNvIHRoYXQgcmVzdWx0cyBpbiBjb2RlIHN1Y2ggYXM6XG4gICAqXG4gICAqIGBgYFxuICAgKiBsZXQgTXlDbGFzc18xOyBsZXQgTXlDbGFzcyA9IE15Q2xhc3NfMSA9IGNsYXNzIE15Q2xhc3Mge307XG4gICAqIGBgYFxuICAgKlxuICAgKiBUaGlzIG1hcCB0cmFja3MgdGhvc2UgYWxpYXNlZCB2YXJpYWJsZXMgdG8gdGhlaXIgb3JpZ2luYWwgaWRlbnRpZmllciwgaS5lLiB0aGUga2V5XG4gICAqIGNvcnJlc3BvbmRzIHdpdGggdGhlIGRlY2xhcmF0aW9uIG9mIGBNeUNsYXNzXzFgIGFuZCBpdHMgdmFsdWUgYmVjb21lcyB0aGUgYE15Q2xhc3NgIGlkZW50aWZpZXJcbiAgICogb2YgdGhlIHZhcmlhYmxlIGRlY2xhcmF0aW9uLlxuICAgKlxuICAgKiBUaGlzIG1hcCBpcyBwb3B1bGF0ZWQgZHVyaW5nIHRoZSBwcmVwcm9jZXNzaW5nIG9mIGVhY2ggc291cmNlIGZpbGUuXG4gICAqL1xuICBwcm90ZWN0ZWQgYWxpYXNlZENsYXNzRGVjbGFyYXRpb25zID0gbmV3IE1hcDx0cy5EZWNsYXJhdGlvbiwgdHMuSWRlbnRpZmllcj4oKTtcblxuICBjb25zdHJ1Y3RvcihcbiAgICAgIHByb3RlY3RlZCBsb2dnZXI6IExvZ2dlciwgcHJvdGVjdGVkIGlzQ29yZTogYm9vbGVhbiwgY2hlY2tlcjogdHMuVHlwZUNoZWNrZXIsXG4gICAgICBkdHM/OiBCdW5kbGVQcm9ncmFtfG51bGwpIHtcbiAgICBzdXBlcihjaGVja2VyKTtcbiAgICB0aGlzLmR0c0RlY2xhcmF0aW9uTWFwID0gZHRzICYmIHRoaXMuY29tcHV0ZUR0c0RlY2xhcmF0aW9uTWFwKGR0cy5wYXRoLCBkdHMucHJvZ3JhbSkgfHwgbnVsbDtcbiAgfVxuXG4gIC8qKlxuICAgKiBGaW5kIHRoZSBkZWNsYXJhdGlvbiBvZiBhIG5vZGUgdGhhdCB3ZSB0aGluayBpcyBhIGNsYXNzLlxuICAgKiBDbGFzc2VzIHNob3VsZCBoYXZlIGEgYG5hbWVgIGlkZW50aWZpZXIsIGJlY2F1c2UgdGhleSBtYXkgbmVlZCB0byBiZSByZWZlcmVuY2VkIGluIG90aGVyIHBhcnRzXG4gICAqIG9mIHRoZSBwcm9ncmFtLlxuICAgKlxuICAgKiBJbiBFUzIwMTUsIGEgY2xhc3MgbWF5IGJlIGRlY2xhcmVkIHVzaW5nIGEgdmFyaWFibGUgZGVjbGFyYXRpb24gb2YgdGhlIGZvbGxvd2luZyBzdHJ1Y3R1cmU6XG4gICAqXG4gICAqIGBgYFxuICAgKiB2YXIgTXlDbGFzcyA9IE15Q2xhc3NfMSA9IGNsYXNzIE15Q2xhc3Mge307XG4gICAqIGBgYFxuICAgKlxuICAgKiBIZXJlLCB0aGUgaW50ZXJtZWRpYXRlIGBNeUNsYXNzXzFgIGFzc2lnbm1lbnQgaXMgb3B0aW9uYWwuIEluIHRoZSBhYm92ZSBleGFtcGxlLCB0aGVcbiAgICogYGNsYXNzIE15Q2xhc3Mge31gIG5vZGUgaXMgcmV0dXJuZWQgYXMgZGVjbGFyYXRpb24gb2YgYE15Q2xhc3NgLlxuICAgKlxuICAgKiBAcGFyYW0gbm9kZSB0aGUgbm9kZSB0aGF0IHJlcHJlc2VudHMgdGhlIGNsYXNzIHdob3NlIGRlY2xhcmF0aW9uIHdlIGFyZSBmaW5kaW5nLlxuICAgKiBAcmV0dXJucyB0aGUgZGVjbGFyYXRpb24gb2YgdGhlIGNsYXNzIG9yIGB1bmRlZmluZWRgIGlmIGl0IGlzIG5vdCBhIFwiY2xhc3NcIi5cbiAgICovXG4gIGdldENsYXNzRGVjbGFyYXRpb24obm9kZTogdHMuTm9kZSk6IENsYXNzRGVjbGFyYXRpb258dW5kZWZpbmVkIHtcbiAgICByZXR1cm4gZ2V0SW5uZXJDbGFzc0RlY2xhcmF0aW9uKG5vZGUpIHx8IHVuZGVmaW5lZDtcbiAgfVxuXG4gIC8qKlxuICAgKiBGaW5kIGEgc3ltYm9sIGZvciBhIG5vZGUgdGhhdCB3ZSB0aGluayBpcyBhIGNsYXNzLlxuICAgKiBAcGFyYW0gbm9kZSB0aGUgbm9kZSB3aG9zZSBzeW1ib2wgd2UgYXJlIGZpbmRpbmcuXG4gICAqIEByZXR1cm5zIHRoZSBzeW1ib2wgZm9yIHRoZSBub2RlIG9yIGB1bmRlZmluZWRgIGlmIGl0IGlzIG5vdCBhIFwiY2xhc3NcIiBvciBoYXMgbm8gc3ltYm9sLlxuICAgKi9cbiAgZ2V0Q2xhc3NTeW1ib2woZGVjbGFyYXRpb246IHRzLk5vZGUpOiBDbGFzc1N5bWJvbHx1bmRlZmluZWQge1xuICAgIGNvbnN0IGNsYXNzRGVjbGFyYXRpb24gPSB0aGlzLmdldENsYXNzRGVjbGFyYXRpb24oZGVjbGFyYXRpb24pO1xuICAgIHJldHVybiBjbGFzc0RlY2xhcmF0aW9uICYmXG4gICAgICAgIHRoaXMuY2hlY2tlci5nZXRTeW1ib2xBdExvY2F0aW9uKGNsYXNzRGVjbGFyYXRpb24ubmFtZSkgYXMgQ2xhc3NTeW1ib2w7XG4gIH1cblxuICAvKipcbiAgICogRXhhbWluZSBhIGRlY2xhcmF0aW9uIChmb3IgZXhhbXBsZSwgb2YgYSBjbGFzcyBvciBmdW5jdGlvbikgYW5kIHJldHVybiBtZXRhZGF0YSBhYm91dCBhbnlcbiAgICogZGVjb3JhdG9ycyBwcmVzZW50IG9uIHRoZSBkZWNsYXJhdGlvbi5cbiAgICpcbiAgICogQHBhcmFtIGRlY2xhcmF0aW9uIGEgVHlwZVNjcmlwdCBgdHMuRGVjbGFyYXRpb25gIG5vZGUgcmVwcmVzZW50aW5nIHRoZSBjbGFzcyBvciBmdW5jdGlvbiBvdmVyXG4gICAqIHdoaWNoIHRvIHJlZmxlY3QuIEZvciBleGFtcGxlLCBpZiB0aGUgaW50ZW50IGlzIHRvIHJlZmxlY3QgdGhlIGRlY29yYXRvcnMgb2YgYSBjbGFzcyBhbmQgdGhlXG4gICAqIHNvdXJjZSBpcyBpbiBFUzYgZm9ybWF0LCB0aGlzIHdpbGwgYmUgYSBgdHMuQ2xhc3NEZWNsYXJhdGlvbmAgbm9kZS4gSWYgdGhlIHNvdXJjZSBpcyBpbiBFUzVcbiAgICogZm9ybWF0LCB0aGlzIG1pZ2h0IGJlIGEgYHRzLlZhcmlhYmxlRGVjbGFyYXRpb25gIGFzIGNsYXNzZXMgaW4gRVM1IGFyZSByZXByZXNlbnRlZCBhcyB0aGVcbiAgICogcmVzdWx0IG9mIGFuIElJRkUgZXhlY3V0aW9uLlxuICAgKlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBgRGVjb3JhdG9yYCBtZXRhZGF0YSBpZiBkZWNvcmF0b3JzIGFyZSBwcmVzZW50IG9uIHRoZSBkZWNsYXJhdGlvbiwgb3JcbiAgICogYG51bGxgIGlmIGVpdGhlciBubyBkZWNvcmF0b3JzIHdlcmUgcHJlc2VudCBvciBpZiB0aGUgZGVjbGFyYXRpb24gaXMgbm90IG9mIGEgZGVjb3JhdGFibGUgdHlwZS5cbiAgICovXG4gIGdldERlY29yYXRvcnNPZkRlY2xhcmF0aW9uKGRlY2xhcmF0aW9uOiB0cy5EZWNsYXJhdGlvbik6IERlY29yYXRvcltdfG51bGwge1xuICAgIGNvbnN0IHN5bWJvbCA9IHRoaXMuZ2V0Q2xhc3NTeW1ib2woZGVjbGFyYXRpb24pO1xuICAgIGlmICghc3ltYm9sKSB7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG4gICAgcmV0dXJuIHRoaXMuZ2V0RGVjb3JhdG9yc09mU3ltYm9sKHN5bWJvbCk7XG4gIH1cblxuICAvKipcbiAgICogRXhhbWluZSBhIGRlY2xhcmF0aW9uIHdoaWNoIHNob3VsZCBiZSBvZiBhIGNsYXNzLCBhbmQgcmV0dXJuIG1ldGFkYXRhIGFib3V0IHRoZSBtZW1iZXJzIG9mIHRoZVxuICAgKiBjbGFzcy5cbiAgICpcbiAgICogQHBhcmFtIGNsYXp6IGEgYENsYXNzRGVjbGFyYXRpb25gIHJlcHJlc2VudGluZyB0aGUgY2xhc3Mgb3ZlciB3aGljaCB0byByZWZsZWN0LlxuICAgKlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBgQ2xhc3NNZW1iZXJgIG1ldGFkYXRhIHJlcHJlc2VudGluZyB0aGUgbWVtYmVycyBvZiB0aGUgY2xhc3MuXG4gICAqXG4gICAqIEB0aHJvd3MgaWYgYGRlY2xhcmF0aW9uYCBkb2VzIG5vdCByZXNvbHZlIHRvIGEgY2xhc3MgZGVjbGFyYXRpb24uXG4gICAqL1xuICBnZXRNZW1iZXJzT2ZDbGFzcyhjbGF6ejogQ2xhc3NEZWNsYXJhdGlvbik6IENsYXNzTWVtYmVyW10ge1xuICAgIGNvbnN0IGNsYXNzU3ltYm9sID0gdGhpcy5nZXRDbGFzc1N5bWJvbChjbGF6eik7XG4gICAgaWYgKCFjbGFzc1N5bWJvbCkge1xuICAgICAgdGhyb3cgbmV3IEVycm9yKGBBdHRlbXB0ZWQgdG8gZ2V0IG1lbWJlcnMgb2YgYSBub24tY2xhc3M6IFwiJHtjbGF6ei5nZXRUZXh0KCl9XCJgKTtcbiAgICB9XG5cbiAgICByZXR1cm4gdGhpcy5nZXRNZW1iZXJzT2ZTeW1ib2woY2xhc3NTeW1ib2wpO1xuICB9XG5cbiAgLyoqXG4gICAqIFJlZmxlY3Qgb3ZlciB0aGUgY29uc3RydWN0b3Igb2YgYSBjbGFzcyBhbmQgcmV0dXJuIG1ldGFkYXRhIGFib3V0IGl0cyBwYXJhbWV0ZXJzLlxuICAgKlxuICAgKiBUaGlzIG1ldGhvZCBvbmx5IGxvb2tzIGF0IHRoZSBjb25zdHJ1Y3RvciBvZiBhIGNsYXNzIGRpcmVjdGx5IGFuZCBub3QgYXQgYW55IGluaGVyaXRlZFxuICAgKiBjb25zdHJ1Y3RvcnMuXG4gICAqXG4gICAqIEBwYXJhbSBjbGF6eiBhIGBDbGFzc0RlY2xhcmF0aW9uYCByZXByZXNlbnRpbmcgdGhlIGNsYXNzIG92ZXIgd2hpY2ggdG8gcmVmbGVjdC5cbiAgICpcbiAgICogQHJldHVybnMgYW4gYXJyYXkgb2YgYFBhcmFtZXRlcmAgbWV0YWRhdGEgcmVwcmVzZW50aW5nIHRoZSBwYXJhbWV0ZXJzIG9mIHRoZSBjb25zdHJ1Y3RvciwgaWZcbiAgICogYSBjb25zdHJ1Y3RvciBleGlzdHMuIElmIHRoZSBjb25zdHJ1Y3RvciBleGlzdHMgYW5kIGhhcyAwIHBhcmFtZXRlcnMsIHRoaXMgYXJyYXkgd2lsbCBiZSBlbXB0eS5cbiAgICogSWYgdGhlIGNsYXNzIGhhcyBubyBjb25zdHJ1Y3RvciwgdGhpcyBtZXRob2QgcmV0dXJucyBgbnVsbGAuXG4gICAqXG4gICAqIEB0aHJvd3MgaWYgYGRlY2xhcmF0aW9uYCBkb2VzIG5vdCByZXNvbHZlIHRvIGEgY2xhc3MgZGVjbGFyYXRpb24uXG4gICAqL1xuICBnZXRDb25zdHJ1Y3RvclBhcmFtZXRlcnMoY2xheno6IENsYXNzRGVjbGFyYXRpb24pOiBDdG9yUGFyYW1ldGVyW118bnVsbCB7XG4gICAgY29uc3QgY2xhc3NTeW1ib2wgPSB0aGlzLmdldENsYXNzU3ltYm9sKGNsYXp6KTtcbiAgICBpZiAoIWNsYXNzU3ltYm9sKSB7XG4gICAgICB0aHJvdyBuZXcgRXJyb3IoXG4gICAgICAgICAgYEF0dGVtcHRlZCB0byBnZXQgY29uc3RydWN0b3IgcGFyYW1ldGVycyBvZiBhIG5vbi1jbGFzczogXCIke2NsYXp6LmdldFRleHQoKX1cImApO1xuICAgIH1cbiAgICBjb25zdCBwYXJhbWV0ZXJOb2RlcyA9IHRoaXMuZ2V0Q29uc3RydWN0b3JQYXJhbWV0ZXJEZWNsYXJhdGlvbnMoY2xhc3NTeW1ib2wpO1xuICAgIGlmIChwYXJhbWV0ZXJOb2Rlcykge1xuICAgICAgcmV0dXJuIHRoaXMuZ2V0Q29uc3RydWN0b3JQYXJhbUluZm8oY2xhc3NTeW1ib2wsIHBhcmFtZXRlck5vZGVzKTtcbiAgICB9XG4gICAgcmV0dXJuIG51bGw7XG4gIH1cblxuICBoYXNCYXNlQ2xhc3MoY2xheno6IENsYXNzRGVjbGFyYXRpb24pOiBib29sZWFuIHtcbiAgICBjb25zdCBzdXBlckhhc0Jhc2VDbGFzcyA9IHN1cGVyLmhhc0Jhc2VDbGFzcyhjbGF6eik7XG4gICAgaWYgKHN1cGVySGFzQmFzZUNsYXNzKSB7XG4gICAgICByZXR1cm4gc3VwZXJIYXNCYXNlQ2xhc3M7XG4gICAgfVxuXG4gICAgY29uc3QgaW5uZXJDbGFzc0RlY2xhcmF0aW9uID0gZ2V0SW5uZXJDbGFzc0RlY2xhcmF0aW9uKGNsYXp6KTtcbiAgICBpZiAoaW5uZXJDbGFzc0RlY2xhcmF0aW9uID09PSBudWxsKSB7XG4gICAgICByZXR1cm4gZmFsc2U7XG4gICAgfVxuXG4gICAgcmV0dXJuIGlubmVyQ2xhc3NEZWNsYXJhdGlvbi5oZXJpdGFnZUNsYXVzZXMgIT09IHVuZGVmaW5lZCAmJlxuICAgICAgICBpbm5lckNsYXNzRGVjbGFyYXRpb24uaGVyaXRhZ2VDbGF1c2VzLnNvbWUoXG4gICAgICAgICAgICBjbGF1c2UgPT4gY2xhdXNlLnRva2VuID09PSB0cy5TeW50YXhLaW5kLkV4dGVuZHNLZXl3b3JkKTtcbiAgfVxuXG4gIC8qKlxuICAgKiBDaGVjayB3aGV0aGVyIHRoZSBnaXZlbiBub2RlIGFjdHVhbGx5IHJlcHJlc2VudHMgYSBjbGFzcy5cbiAgICovXG4gIGlzQ2xhc3Mobm9kZTogdHMuTm9kZSk6IG5vZGUgaXMgQ2xhc3NEZWNsYXJhdGlvbiB7XG4gICAgcmV0dXJuIHN1cGVyLmlzQ2xhc3Mobm9kZSkgfHwgISF0aGlzLmdldENsYXNzRGVjbGFyYXRpb24obm9kZSk7XG4gIH1cblxuICAvKipcbiAgICogVHJhY2UgYW4gaWRlbnRpZmllciB0byBpdHMgZGVjbGFyYXRpb24sIGlmIHBvc3NpYmxlLlxuICAgKlxuICAgKiBUaGlzIG1ldGhvZCBhdHRlbXB0cyB0byByZXNvbHZlIHRoZSBkZWNsYXJhdGlvbiBvZiB0aGUgZ2l2ZW4gaWRlbnRpZmllciwgdHJhY2luZyBiYWNrIHRocm91Z2hcbiAgICogaW1wb3J0cyBhbmQgcmUtZXhwb3J0cyB1bnRpbCB0aGUgb3JpZ2luYWwgZGVjbGFyYXRpb24gc3RhdGVtZW50IGlzIGZvdW5kLiBBIGBEZWNsYXJhdGlvbmBcbiAgICogb2JqZWN0IGlzIHJldHVybmVkIGlmIHRoZSBvcmlnaW5hbCBkZWNsYXJhdGlvbiBpcyBmb3VuZCwgb3IgYG51bGxgIGlzIHJldHVybmVkIG90aGVyd2lzZS5cbiAgICpcbiAgICogSW4gRVMyMDE1LCB3ZSBuZWVkIHRvIGFjY291bnQgZm9yIGlkZW50aWZpZXJzIHRoYXQgcmVmZXIgdG8gYWxpYXNlZCBjbGFzcyBkZWNsYXJhdGlvbnMgc3VjaCBhc1xuICAgKiBgTXlDbGFzc18xYC4gU2luY2Ugc3VjaCBkZWNsYXJhdGlvbnMgYXJlIG9ubHkgYXZhaWxhYmxlIHdpdGhpbiB0aGUgbW9kdWxlIGl0c2VsZiwgd2UgbmVlZCB0b1xuICAgKiBmaW5kIHRoZSBvcmlnaW5hbCBjbGFzcyBkZWNsYXJhdGlvbiwgZS5nLiBgTXlDbGFzc2AsIHRoYXQgaXMgYXNzb2NpYXRlZCB3aXRoIHRoZSBhbGlhc2VkIG9uZS5cbiAgICpcbiAgICogQHBhcmFtIGlkIGEgVHlwZVNjcmlwdCBgdHMuSWRlbnRpZmllcmAgdG8gdHJhY2UgYmFjayB0byBhIGRlY2xhcmF0aW9uLlxuICAgKlxuICAgKiBAcmV0dXJucyBtZXRhZGF0YSBhYm91dCB0aGUgYERlY2xhcmF0aW9uYCBpZiB0aGUgb3JpZ2luYWwgZGVjbGFyYXRpb24gaXMgZm91bmQsIG9yIGBudWxsYFxuICAgKiBvdGhlcndpc2UuXG4gICAqL1xuICBnZXREZWNsYXJhdGlvbk9mSWRlbnRpZmllcihpZDogdHMuSWRlbnRpZmllcik6IERlY2xhcmF0aW9ufG51bGwge1xuICAgIGNvbnN0IHN1cGVyRGVjbGFyYXRpb24gPSBzdXBlci5nZXREZWNsYXJhdGlvbk9mSWRlbnRpZmllcihpZCk7XG5cbiAgICAvLyBUaGUgaWRlbnRpZmllciBtYXkgaGF2ZSBiZWVuIG9mIGFuIGFkZGl0aW9uYWwgY2xhc3MgYXNzaWdubWVudCBzdWNoIGFzIGBNeUNsYXNzXzFgIHRoYXQgd2FzXG4gICAgLy8gcHJlc2VudCBhcyBhbGlhcyBmb3IgYE15Q2xhc3NgLiBJZiBzbywgcmVzb2x2ZSBzdWNoIGFsaWFzZXMgdG8gdGhlaXIgb3JpZ2luYWwgZGVjbGFyYXRpb24uXG4gICAgaWYgKHN1cGVyRGVjbGFyYXRpb24gIT09IG51bGwpIHtcbiAgICAgIGNvbnN0IGFsaWFzZWRJZGVudGlmaWVyID0gdGhpcy5yZXNvbHZlQWxpYXNlZENsYXNzSWRlbnRpZmllcihzdXBlckRlY2xhcmF0aW9uLm5vZGUpO1xuICAgICAgaWYgKGFsaWFzZWRJZGVudGlmaWVyICE9PSBudWxsKSB7XG4gICAgICAgIHJldHVybiB0aGlzLmdldERlY2xhcmF0aW9uT2ZJZGVudGlmaWVyKGFsaWFzZWRJZGVudGlmaWVyKTtcbiAgICAgIH1cbiAgICB9XG5cbiAgICByZXR1cm4gc3VwZXJEZWNsYXJhdGlvbjtcbiAgfVxuXG4gIC8qKiBHZXRzIGFsbCBkZWNvcmF0b3JzIG9mIHRoZSBnaXZlbiBjbGFzcyBzeW1ib2wuICovXG4gIGdldERlY29yYXRvcnNPZlN5bWJvbChzeW1ib2w6IENsYXNzU3ltYm9sKTogRGVjb3JhdG9yW118bnVsbCB7XG4gICAgY29uc3QgZGVjb3JhdG9yc1Byb3BlcnR5ID0gdGhpcy5nZXRTdGF0aWNQcm9wZXJ0eShzeW1ib2wsIERFQ09SQVRPUlMpO1xuICAgIGlmIChkZWNvcmF0b3JzUHJvcGVydHkpIHtcbiAgICAgIHJldHVybiB0aGlzLmdldENsYXNzRGVjb3JhdG9yc0Zyb21TdGF0aWNQcm9wZXJ0eShkZWNvcmF0b3JzUHJvcGVydHkpO1xuICAgIH0gZWxzZSB7XG4gICAgICByZXR1cm4gdGhpcy5nZXRDbGFzc0RlY29yYXRvcnNGcm9tSGVscGVyQ2FsbChzeW1ib2wpO1xuICAgIH1cbiAgfVxuXG4gIC8qKlxuICAgKiBTZWFyY2ggdGhlIGdpdmVuIG1vZHVsZSBmb3IgdmFyaWFibGUgZGVjbGFyYXRpb25zIGluIHdoaWNoIHRoZSBpbml0aWFsaXplclxuICAgKiBpcyBhbiBpZGVudGlmaWVyIG1hcmtlZCB3aXRoIHRoZSBgUFJFX1IzX01BUktFUmAuXG4gICAqIEBwYXJhbSBtb2R1bGUgdGhlIG1vZHVsZSBpbiB3aGljaCB0byBzZWFyY2ggZm9yIHN3aXRjaGFibGUgZGVjbGFyYXRpb25zLlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiB2YXJpYWJsZSBkZWNsYXJhdGlvbnMgdGhhdCBtYXRjaC5cbiAgICovXG4gIGdldFN3aXRjaGFibGVEZWNsYXJhdGlvbnMobW9kdWxlOiB0cy5Ob2RlKTogU3dpdGNoYWJsZVZhcmlhYmxlRGVjbGFyYXRpb25bXSB7XG4gICAgLy8gRG9uJ3QgYm90aGVyIHRvIHdhbGsgdGhlIEFTVCBpZiB0aGUgbWFya2VyIGlzIG5vdCBmb3VuZCBpbiB0aGUgdGV4dFxuICAgIHJldHVybiBtb2R1bGUuZ2V0VGV4dCgpLmluZGV4T2YoUFJFX1IzX01BUktFUikgPj0gMCA/XG4gICAgICAgIGZpbmRBbGwobW9kdWxlLCBpc1N3aXRjaGFibGVWYXJpYWJsZURlY2xhcmF0aW9uKSA6XG4gICAgICAgIFtdO1xuICB9XG5cbiAgZ2V0VmFyaWFibGVWYWx1ZShkZWNsYXJhdGlvbjogdHMuVmFyaWFibGVEZWNsYXJhdGlvbik6IHRzLkV4cHJlc3Npb258bnVsbCB7XG4gICAgY29uc3QgdmFsdWUgPSBzdXBlci5nZXRWYXJpYWJsZVZhbHVlKGRlY2xhcmF0aW9uKTtcbiAgICBpZiAodmFsdWUpIHtcbiAgICAgIHJldHVybiB2YWx1ZTtcbiAgICB9XG5cbiAgICAvLyBXZSBoYXZlIGEgdmFyaWFibGUgZGVjbGFyYXRpb24gdGhhdCBoYXMgbm8gaW5pdGlhbGl6ZXIuIEZvciBleGFtcGxlOlxuICAgIC8vXG4gICAgLy8gYGBgXG4gICAgLy8gdmFyIEh0dHBDbGllbnRYc3JmTW9kdWxlXzE7XG4gICAgLy8gYGBgXG4gICAgLy9cbiAgICAvLyBTbyBsb29rIGZvciB0aGUgc3BlY2lhbCBzY2VuYXJpbyB3aGVyZSB0aGUgdmFyaWFibGUgaXMgYmVpbmcgYXNzaWduZWQgaW5cbiAgICAvLyBhIG5lYXJieSBzdGF0ZW1lbnQgdG8gdGhlIHJldHVybiB2YWx1ZSBvZiBhIGNhbGwgdG8gYF9fZGVjb3JhdGVgLlxuICAgIC8vIFRoZW4gZmluZCB0aGUgMm5kIGFyZ3VtZW50IG9mIHRoYXQgY2FsbCwgdGhlIFwidGFyZ2V0XCIsIHdoaWNoIHdpbGwgYmUgdGhlXG4gICAgLy8gYWN0dWFsIGNsYXNzIGlkZW50aWZpZXIuIEZvciBleGFtcGxlOlxuICAgIC8vXG4gICAgLy8gYGBgXG4gICAgLy8gSHR0cENsaWVudFhzcmZNb2R1bGUgPSBIdHRwQ2xpZW50WHNyZk1vZHVsZV8xID0gdHNsaWJfMS5fX2RlY29yYXRlKFtcbiAgICAvLyAgIE5nTW9kdWxlKHtcbiAgICAvLyAgICAgcHJvdmlkZXJzOiBbXSxcbiAgICAvLyAgIH0pXG4gICAgLy8gXSwgSHR0cENsaWVudFhzcmZNb2R1bGUpO1xuICAgIC8vIGBgYFxuICAgIC8vXG4gICAgLy8gQW5kIGZpbmFsbHksIGZpbmQgdGhlIGRlY2xhcmF0aW9uIG9mIHRoZSBpZGVudGlmaWVyIGluIHRoYXQgYXJndW1lbnQuXG4gICAgLy8gTm90ZSBhbHNvIHRoYXQgdGhlIGFzc2lnbm1lbnQgY2FuIG9jY3VyIHdpdGhpbiBhbm90aGVyIGFzc2lnbm1lbnQuXG4gICAgLy9cbiAgICBjb25zdCBibG9jayA9IGRlY2xhcmF0aW9uLnBhcmVudC5wYXJlbnQucGFyZW50O1xuICAgIGNvbnN0IHN5bWJvbCA9IHRoaXMuY2hlY2tlci5nZXRTeW1ib2xBdExvY2F0aW9uKGRlY2xhcmF0aW9uLm5hbWUpO1xuICAgIGlmIChzeW1ib2wgJiYgKHRzLmlzQmxvY2soYmxvY2spIHx8IHRzLmlzU291cmNlRmlsZShibG9jaykpKSB7XG4gICAgICBjb25zdCBkZWNvcmF0ZUNhbGwgPSB0aGlzLmZpbmREZWNvcmF0ZWRWYXJpYWJsZVZhbHVlKGJsb2NrLCBzeW1ib2wpO1xuICAgICAgY29uc3QgdGFyZ2V0ID0gZGVjb3JhdGVDYWxsICYmIGRlY29yYXRlQ2FsbC5hcmd1bWVudHNbMV07XG4gICAgICBpZiAodGFyZ2V0ICYmIHRzLmlzSWRlbnRpZmllcih0YXJnZXQpKSB7XG4gICAgICAgIGNvbnN0IHRhcmdldFN5bWJvbCA9IHRoaXMuY2hlY2tlci5nZXRTeW1ib2xBdExvY2F0aW9uKHRhcmdldCk7XG4gICAgICAgIGNvbnN0IHRhcmdldERlY2xhcmF0aW9uID0gdGFyZ2V0U3ltYm9sICYmIHRhcmdldFN5bWJvbC52YWx1ZURlY2xhcmF0aW9uO1xuICAgICAgICBpZiAodGFyZ2V0RGVjbGFyYXRpb24pIHtcbiAgICAgICAgICBpZiAodHMuaXNDbGFzc0RlY2xhcmF0aW9uKHRhcmdldERlY2xhcmF0aW9uKSB8fFxuICAgICAgICAgICAgICB0cy5pc0Z1bmN0aW9uRGVjbGFyYXRpb24odGFyZ2V0RGVjbGFyYXRpb24pKSB7XG4gICAgICAgICAgICAvLyBUaGUgdGFyZ2V0IGlzIGp1c3QgYSBmdW5jdGlvbiBvciBjbGFzcyBkZWNsYXJhdGlvblxuICAgICAgICAgICAgLy8gc28gcmV0dXJuIGl0cyBpZGVudGlmaWVyIGFzIHRoZSB2YXJpYWJsZSB2YWx1ZS5cbiAgICAgICAgICAgIHJldHVybiB0YXJnZXREZWNsYXJhdGlvbi5uYW1lIHx8IG51bGw7XG4gICAgICAgICAgfSBlbHNlIGlmICh0cy5pc1ZhcmlhYmxlRGVjbGFyYXRpb24odGFyZ2V0RGVjbGFyYXRpb24pKSB7XG4gICAgICAgICAgICAvLyBUaGUgdGFyZ2V0IGlzIGEgdmFyaWFibGUgZGVjbGFyYXRpb24sIHNvIGZpbmQgdGhlIGZhciByaWdodCBleHByZXNzaW9uLFxuICAgICAgICAgICAgLy8gaW4gdGhlIGNhc2Ugb2YgbXVsdGlwbGUgYXNzaWdubWVudHMgKGUuZy4gYHZhcjEgPSB2YXIyID0gdmFsdWVgKS5cbiAgICAgICAgICAgIGxldCB0YXJnZXRWYWx1ZSA9IHRhcmdldERlY2xhcmF0aW9uLmluaXRpYWxpemVyO1xuICAgICAgICAgICAgd2hpbGUgKHRhcmdldFZhbHVlICYmIGlzQXNzaWdubWVudCh0YXJnZXRWYWx1ZSkpIHtcbiAgICAgICAgICAgICAgdGFyZ2V0VmFsdWUgPSB0YXJnZXRWYWx1ZS5yaWdodDtcbiAgICAgICAgICAgIH1cbiAgICAgICAgICAgIGlmICh0YXJnZXRWYWx1ZSkge1xuICAgICAgICAgICAgICByZXR1cm4gdGFyZ2V0VmFsdWU7XG4gICAgICAgICAgICB9XG4gICAgICAgICAgfVxuICAgICAgICB9XG4gICAgICB9XG4gICAgfVxuICAgIHJldHVybiBudWxsO1xuICB9XG5cbiAgLyoqXG4gICAqIEZpbmQgYWxsIHRvcC1sZXZlbCBjbGFzcyBzeW1ib2xzIGluIHRoZSBnaXZlbiBmaWxlLlxuICAgKiBAcGFyYW0gc291cmNlRmlsZSBUaGUgc291cmNlIGZpbGUgdG8gc2VhcmNoIGZvciBjbGFzc2VzLlxuICAgKiBAcmV0dXJucyBBbiBhcnJheSBvZiBjbGFzcyBzeW1ib2xzLlxuICAgKi9cbiAgZmluZENsYXNzU3ltYm9scyhzb3VyY2VGaWxlOiB0cy5Tb3VyY2VGaWxlKTogQ2xhc3NTeW1ib2xbXSB7XG4gICAgY29uc3QgY2xhc3NlczogQ2xhc3NTeW1ib2xbXSA9IFtdO1xuICAgIHRoaXMuZ2V0TW9kdWxlU3RhdGVtZW50cyhzb3VyY2VGaWxlKS5mb3JFYWNoKHN0YXRlbWVudCA9PiB7XG4gICAgICBpZiAodHMuaXNWYXJpYWJsZVN0YXRlbWVudChzdGF0ZW1lbnQpKSB7XG4gICAgICAgIHN0YXRlbWVudC5kZWNsYXJhdGlvbkxpc3QuZGVjbGFyYXRpb25zLmZvckVhY2goZGVjbGFyYXRpb24gPT4ge1xuICAgICAgICAgIGNvbnN0IGNsYXNzU3ltYm9sID0gdGhpcy5nZXRDbGFzc1N5bWJvbChkZWNsYXJhdGlvbik7XG4gICAgICAgICAgaWYgKGNsYXNzU3ltYm9sKSB7XG4gICAgICAgICAgICBjbGFzc2VzLnB1c2goY2xhc3NTeW1ib2wpO1xuICAgICAgICAgIH1cbiAgICAgICAgfSk7XG4gICAgICB9IGVsc2UgaWYgKHRzLmlzQ2xhc3NEZWNsYXJhdGlvbihzdGF0ZW1lbnQpKSB7XG4gICAgICAgIGNvbnN0IGNsYXNzU3ltYm9sID0gdGhpcy5nZXRDbGFzc1N5bWJvbChzdGF0ZW1lbnQpO1xuICAgICAgICBpZiAoY2xhc3NTeW1ib2wpIHtcbiAgICAgICAgICBjbGFzc2VzLnB1c2goY2xhc3NTeW1ib2wpO1xuICAgICAgICB9XG4gICAgICB9XG4gICAgfSk7XG4gICAgcmV0dXJuIGNsYXNzZXM7XG4gIH1cblxuICAvKipcbiAgICogR2V0IHRoZSBudW1iZXIgb2YgZ2VuZXJpYyB0eXBlIHBhcmFtZXRlcnMgb2YgYSBnaXZlbiBjbGFzcy5cbiAgICpcbiAgICogQHBhcmFtIGNsYXp6IGEgYENsYXNzRGVjbGFyYXRpb25gIHJlcHJlc2VudGluZyB0aGUgY2xhc3Mgb3ZlciB3aGljaCB0byByZWZsZWN0LlxuICAgKlxuICAgKiBAcmV0dXJucyB0aGUgbnVtYmVyIG9mIHR5cGUgcGFyYW1ldGVycyBvZiB0aGUgY2xhc3MsIGlmIGtub3duLCBvciBgbnVsbGAgaWYgdGhlIGRlY2xhcmF0aW9uXG4gICAqIGlzIG5vdCBhIGNsYXNzIG9yIGhhcyBhbiB1bmtub3duIG51bWJlciBvZiB0eXBlIHBhcmFtZXRlcnMuXG4gICAqL1xuICBnZXRHZW5lcmljQXJpdHlPZkNsYXNzKGNsYXp6OiBDbGFzc0RlY2xhcmF0aW9uKTogbnVtYmVyfG51bGwge1xuICAgIGNvbnN0IGR0c0RlY2xhcmF0aW9uID0gdGhpcy5nZXREdHNEZWNsYXJhdGlvbihjbGF6eik7XG4gICAgaWYgKGR0c0RlY2xhcmF0aW9uICYmIHRzLmlzQ2xhc3NEZWNsYXJhdGlvbihkdHNEZWNsYXJhdGlvbikpIHtcbiAgICAgIHJldHVybiBkdHNEZWNsYXJhdGlvbi50eXBlUGFyYW1ldGVycyA/IGR0c0RlY2xhcmF0aW9uLnR5cGVQYXJhbWV0ZXJzLmxlbmd0aCA6IDA7XG4gICAgfVxuICAgIHJldHVybiBudWxsO1xuICB9XG5cbiAgLyoqXG4gICAqIFRha2UgYW4gZXhwb3J0ZWQgZGVjbGFyYXRpb24gb2YgYSBjbGFzcyAobWF5YmUgZG93bi1sZXZlbGVkIHRvIGEgdmFyaWFibGUpIGFuZCBsb29rIHVwIHRoZVxuICAgKiBkZWNsYXJhdGlvbiBvZiBpdHMgdHlwZSBpbiBhIHNlcGFyYXRlIC5kLnRzIHRyZWUuXG4gICAqXG4gICAqIFRoaXMgZnVuY3Rpb24gaXMgYWxsb3dlZCB0byByZXR1cm4gYG51bGxgIGlmIHRoZSBjdXJyZW50IGNvbXBpbGF0aW9uIHVuaXQgZG9lcyBub3QgaGF2ZSBhXG4gICAqIHNlcGFyYXRlIC5kLnRzIHRyZWUuIFdoZW4gY29tcGlsaW5nIFR5cGVTY3JpcHQgY29kZSB0aGlzIGlzIGFsd2F5cyB0aGUgY2FzZSwgc2luY2UgLmQudHMgZmlsZXNcbiAgICogYXJlIHByb2R1Y2VkIG9ubHkgZHVyaW5nIHRoZSBlbWl0IG9mIHN1Y2ggYSBjb21waWxhdGlvbi4gV2hlbiBjb21waWxpbmcgLmpzIGNvZGUsIGhvd2V2ZXIsXG4gICAqIHRoZXJlIGlzIGZyZXF1ZW50bHkgYSBwYXJhbGxlbCAuZC50cyB0cmVlIHdoaWNoIHRoaXMgbWV0aG9kIGV4cG9zZXMuXG4gICAqXG4gICAqIE5vdGUgdGhhdCB0aGUgYHRzLkNsYXNzRGVjbGFyYXRpb25gIHJldHVybmVkIGZyb20gdGhpcyBmdW5jdGlvbiBtYXkgbm90IGJlIGZyb20gdGhlIHNhbWVcbiAgICogYHRzLlByb2dyYW1gIGFzIHRoZSBpbnB1dCBkZWNsYXJhdGlvbi5cbiAgICovXG4gIGdldER0c0RlY2xhcmF0aW9uKGRlY2xhcmF0aW9uOiB0cy5EZWNsYXJhdGlvbik6IHRzLkRlY2xhcmF0aW9ufG51bGwge1xuICAgIGlmICghdGhpcy5kdHNEZWNsYXJhdGlvbk1hcCkge1xuICAgICAgcmV0dXJuIG51bGw7XG4gICAgfVxuICAgIGlmICghaXNOYW1lZERlY2xhcmF0aW9uKGRlY2xhcmF0aW9uKSkge1xuICAgICAgdGhyb3cgbmV3IEVycm9yKFxuICAgICAgICAgIGBDYW5ub3QgZ2V0IHRoZSBkdHMgZmlsZSBmb3IgYSBkZWNsYXJhdGlvbiB0aGF0IGhhcyBubyBuYW1lOiAke2RlY2xhcmF0aW9uLmdldFRleHQoKX0gaW4gJHtkZWNsYXJhdGlvbi5nZXRTb3VyY2VGaWxlKCkuZmlsZU5hbWV9YCk7XG4gICAgfVxuICAgIHJldHVybiB0aGlzLmR0c0RlY2xhcmF0aW9uTWFwLmhhcyhkZWNsYXJhdGlvbi5uYW1lLnRleHQpID9cbiAgICAgICAgdGhpcy5kdHNEZWNsYXJhdGlvbk1hcC5nZXQoZGVjbGFyYXRpb24ubmFtZS50ZXh0KSAhIDpcbiAgICAgICAgbnVsbDtcbiAgfVxuXG4gIC8qKlxuICAgKiBTZWFyY2ggdGhlIGdpdmVuIHNvdXJjZSBmaWxlIGZvciBleHBvcnRlZCBmdW5jdGlvbnMgYW5kIHN0YXRpYyBjbGFzcyBtZXRob2RzIHRoYXQgcmV0dXJuXG4gICAqIE1vZHVsZVdpdGhQcm92aWRlcnMgb2JqZWN0cy5cbiAgICogQHBhcmFtIGYgVGhlIHNvdXJjZSBmaWxlIHRvIHNlYXJjaCBmb3IgdGhlc2UgZnVuY3Rpb25zXG4gICAqIEByZXR1cm5zIEFuIGFycmF5IG9mIGZ1bmN0aW9uIGRlY2xhcmF0aW9ucyB0aGF0IGxvb2sgbGlrZSB0aGV5IHJldHVybiBNb2R1bGVXaXRoUHJvdmlkZXJzXG4gICAqIG9iamVjdHMuXG4gICAqL1xuICBnZXRNb2R1bGVXaXRoUHJvdmlkZXJzRnVuY3Rpb25zKGY6IHRzLlNvdXJjZUZpbGUpOiBNb2R1bGVXaXRoUHJvdmlkZXJzRnVuY3Rpb25bXSB7XG4gICAgY29uc3QgZXhwb3J0cyA9IHRoaXMuZ2V0RXhwb3J0c09mTW9kdWxlKGYpO1xuICAgIGlmICghZXhwb3J0cykgcmV0dXJuIFtdO1xuICAgIGNvbnN0IGluZm9zOiBNb2R1bGVXaXRoUHJvdmlkZXJzRnVuY3Rpb25bXSA9IFtdO1xuICAgIGV4cG9ydHMuZm9yRWFjaCgoZGVjbGFyYXRpb24sIG5hbWUpID0+IHtcbiAgICAgIGlmICh0aGlzLmlzQ2xhc3MoZGVjbGFyYXRpb24ubm9kZSkpIHtcbiAgICAgICAgdGhpcy5nZXRNZW1iZXJzT2ZDbGFzcyhkZWNsYXJhdGlvbi5ub2RlKS5mb3JFYWNoKG1lbWJlciA9PiB7XG4gICAgICAgICAgaWYgKG1lbWJlci5pc1N0YXRpYykge1xuICAgICAgICAgICAgY29uc3QgaW5mbyA9IHRoaXMucGFyc2VGb3JNb2R1bGVXaXRoUHJvdmlkZXJzKFxuICAgICAgICAgICAgICAgIG1lbWJlci5uYW1lLCBtZW1iZXIubm9kZSwgbWVtYmVyLmltcGxlbWVudGF0aW9uLCBkZWNsYXJhdGlvbi5ub2RlKTtcbiAgICAgICAgICAgIGlmIChpbmZvKSB7XG4gICAgICAgICAgICAgIGluZm9zLnB1c2goaW5mbyk7XG4gICAgICAgICAgICB9XG4gICAgICAgICAgfVxuICAgICAgICB9KTtcbiAgICAgIH0gZWxzZSB7XG4gICAgICAgIGlmIChpc05hbWVkRGVjbGFyYXRpb24oZGVjbGFyYXRpb24ubm9kZSkpIHtcbiAgICAgICAgICBjb25zdCBpbmZvID1cbiAgICAgICAgICAgICAgdGhpcy5wYXJzZUZvck1vZHVsZVdpdGhQcm92aWRlcnMoZGVjbGFyYXRpb24ubm9kZS5uYW1lLnRleHQsIGRlY2xhcmF0aW9uLm5vZGUpO1xuICAgICAgICAgIGlmIChpbmZvKSB7XG4gICAgICAgICAgICBpbmZvcy5wdXNoKGluZm8pO1xuICAgICAgICAgIH1cbiAgICAgICAgfVxuICAgICAgfVxuICAgIH0pO1xuICAgIHJldHVybiBpbmZvcztcbiAgfVxuXG4gIC8vLy8vLy8vLy8vLy8gUHJvdGVjdGVkIEhlbHBlcnMgLy8vLy8vLy8vLy8vL1xuXG4gIC8qKlxuICAgKiBGaW5kcyB0aGUgaWRlbnRpZmllciBvZiB0aGUgYWN0dWFsIGNsYXNzIGRlY2xhcmF0aW9uIGZvciBhIHBvdGVudGlhbGx5IGFsaWFzZWQgZGVjbGFyYXRpb24gb2YgYVxuICAgKiBjbGFzcy5cbiAgICpcbiAgICogSWYgdGhlIGdpdmVuIGRlY2xhcmF0aW9uIGlzIGZvciBhbiBhbGlhcyBvZiBhIGNsYXNzLCB0aGlzIGZ1bmN0aW9uIHdpbGwgZGV0ZXJtaW5lIGFuIGlkZW50aWZpZXJcbiAgICogdG8gdGhlIG9yaWdpbmFsIGRlY2xhcmF0aW9uIHRoYXQgcmVwcmVzZW50cyB0aGlzIGNsYXNzLlxuICAgKlxuICAgKiBAcGFyYW0gZGVjbGFyYXRpb24gVGhlIGRlY2xhcmF0aW9uIHRvIHJlc29sdmUuXG4gICAqIEByZXR1cm5zIFRoZSBvcmlnaW5hbCBpZGVudGlmaWVyIHRoYXQgdGhlIGdpdmVuIGNsYXNzIGRlY2xhcmF0aW9uIHJlc29sdmVzIHRvLCBvciBgdW5kZWZpbmVkYFxuICAgKiBpZiB0aGUgZGVjbGFyYXRpb24gZG9lcyBub3QgcmVwcmVzZW50IGFuIGFsaWFzZWQgY2xhc3MuXG4gICAqL1xuICBwcm90ZWN0ZWQgcmVzb2x2ZUFsaWFzZWRDbGFzc0lkZW50aWZpZXIoZGVjbGFyYXRpb246IHRzLkRlY2xhcmF0aW9uKTogdHMuSWRlbnRpZmllcnxudWxsIHtcbiAgICB0aGlzLmVuc3VyZVByZXByb2Nlc3NlZChkZWNsYXJhdGlvbi5nZXRTb3VyY2VGaWxlKCkpO1xuICAgIHJldHVybiB0aGlzLmFsaWFzZWRDbGFzc0RlY2xhcmF0aW9ucy5oYXMoZGVjbGFyYXRpb24pID9cbiAgICAgICAgdGhpcy5hbGlhc2VkQ2xhc3NEZWNsYXJhdGlvbnMuZ2V0KGRlY2xhcmF0aW9uKSAhIDpcbiAgICAgICAgbnVsbDtcbiAgfVxuXG4gIC8qKlxuICAgKiBFbnN1cmVzIHRoYXQgdGhlIHNvdXJjZSBmaWxlIHRoYXQgYG5vZGVgIGlzIHBhcnQgb2YgaGFzIGJlZW4gcHJlcHJvY2Vzc2VkLlxuICAgKlxuICAgKiBEdXJpbmcgcHJlcHJvY2Vzc2luZywgYWxsIHN0YXRlbWVudHMgaW4gdGhlIHNvdXJjZSBmaWxlIHdpbGwgYmUgdmlzaXRlZCBzdWNoIHRoYXQgY2VydGFpblxuICAgKiBwcm9jZXNzaW5nIHN0ZXBzIGNhbiBiZSBkb25lIHVwLWZyb250IGFuZCBjYWNoZWQgZm9yIHN1YnNlcXVlbnQgdXNhZ2VzLlxuICAgKlxuICAgKiBAcGFyYW0gc291cmNlRmlsZSBUaGUgc291cmNlIGZpbGUgdGhhdCBuZWVkcyB0byBoYXZlIGdvbmUgdGhyb3VnaCBwcmVwcm9jZXNzaW5nLlxuICAgKi9cbiAgcHJvdGVjdGVkIGVuc3VyZVByZXByb2Nlc3NlZChzb3VyY2VGaWxlOiB0cy5Tb3VyY2VGaWxlKTogdm9pZCB7XG4gICAgaWYgKCF0aGlzLnByZXByb2Nlc3NlZFNvdXJjZUZpbGVzLmhhcyhzb3VyY2VGaWxlKSkge1xuICAgICAgdGhpcy5wcmVwcm9jZXNzZWRTb3VyY2VGaWxlcy5hZGQoc291cmNlRmlsZSk7XG5cbiAgICAgIGZvciAoY29uc3Qgc3RhdGVtZW50IG9mIHNvdXJjZUZpbGUuc3RhdGVtZW50cykge1xuICAgICAgICB0aGlzLnByZXByb2Nlc3NTdGF0ZW1lbnQoc3RhdGVtZW50KTtcbiAgICAgIH1cbiAgICB9XG4gIH1cblxuICAvKipcbiAgICogQW5hbHl6ZXMgdGhlIGdpdmVuIHN0YXRlbWVudCB0byBzZWUgaWYgaXQgY29ycmVzcG9uZHMgd2l0aCBhIHZhcmlhYmxlIGRlY2xhcmF0aW9uIGxpa2VcbiAgICogYGxldCBNeUNsYXNzID0gTXlDbGFzc18xID0gY2xhc3MgTXlDbGFzcyB7fTtgLiBJZiBzbywgdGhlIGRlY2xhcmF0aW9uIG9mIGBNeUNsYXNzXzFgXG4gICAqIGlzIGFzc29jaWF0ZWQgd2l0aCB0aGUgYE15Q2xhc3NgIGlkZW50aWZpZXIuXG4gICAqXG4gICAqIEBwYXJhbSBzdGF0ZW1lbnQgVGhlIHN0YXRlbWVudCB0aGF0IG5lZWRzIHRvIGJlIHByZXByb2Nlc3NlZC5cbiAgICovXG4gIHByb3RlY3RlZCBwcmVwcm9jZXNzU3RhdGVtZW50KHN0YXRlbWVudDogdHMuU3RhdGVtZW50KTogdm9pZCB7XG4gICAgaWYgKCF0cy5pc1ZhcmlhYmxlU3RhdGVtZW50KHN0YXRlbWVudCkpIHtcbiAgICAgIHJldHVybjtcbiAgICB9XG5cbiAgICBjb25zdCBkZWNsYXJhdGlvbnMgPSBzdGF0ZW1lbnQuZGVjbGFyYXRpb25MaXN0LmRlY2xhcmF0aW9ucztcbiAgICBpZiAoZGVjbGFyYXRpb25zLmxlbmd0aCAhPT0gMSkge1xuICAgICAgcmV0dXJuO1xuICAgIH1cblxuICAgIGNvbnN0IGRlY2xhcmF0aW9uID0gZGVjbGFyYXRpb25zWzBdO1xuICAgIGNvbnN0IGluaXRpYWxpemVyID0gZGVjbGFyYXRpb24uaW5pdGlhbGl6ZXI7XG4gICAgaWYgKCF0cy5pc0lkZW50aWZpZXIoZGVjbGFyYXRpb24ubmFtZSkgfHwgIWluaXRpYWxpemVyIHx8ICFpc0Fzc2lnbm1lbnQoaW5pdGlhbGl6ZXIpIHx8XG4gICAgICAgICF0cy5pc0lkZW50aWZpZXIoaW5pdGlhbGl6ZXIubGVmdCkgfHwgIXRzLmlzQ2xhc3NFeHByZXNzaW9uKGluaXRpYWxpemVyLnJpZ2h0KSkge1xuICAgICAgcmV0dXJuO1xuICAgIH1cblxuICAgIGNvbnN0IGFsaWFzZWRJZGVudGlmaWVyID0gaW5pdGlhbGl6ZXIubGVmdDtcblxuICAgIGNvbnN0IGFsaWFzZWREZWNsYXJhdGlvbiA9IHRoaXMuZ2V0RGVjbGFyYXRpb25PZklkZW50aWZpZXIoYWxpYXNlZElkZW50aWZpZXIpO1xuICAgIGlmIChhbGlhc2VkRGVjbGFyYXRpb24gPT09IG51bGwpIHtcbiAgICAgIHRocm93IG5ldyBFcnJvcihcbiAgICAgICAgICBgVW5hYmxlIHRvIGxvY2F0ZSBkZWNsYXJhdGlvbiBvZiAke2FsaWFzZWRJZGVudGlmaWVyLnRleHR9IGluIFwiJHtzdGF0ZW1lbnQuZ2V0VGV4dCgpfVwiYCk7XG4gICAgfVxuICAgIHRoaXMuYWxpYXNlZENsYXNzRGVjbGFyYXRpb25zLnNldChhbGlhc2VkRGVjbGFyYXRpb24ubm9kZSwgZGVjbGFyYXRpb24ubmFtZSk7XG4gIH1cblxuICAvKiogR2V0IHRoZSB0b3AgbGV2ZWwgc3RhdGVtZW50cyBmb3IgYSBtb2R1bGUuXG4gICAqXG4gICAqIEluIEVTNSBhbmQgRVMyMDE1IHRoaXMgaXMganVzdCB0aGUgdG9wIGxldmVsIHN0YXRlbWVudHMgb2YgdGhlIGZpbGUuXG4gICAqIEBwYXJhbSBzb3VyY2VGaWxlIFRoZSBtb2R1bGUgd2hvc2Ugc3RhdGVtZW50cyB3ZSB3YW50LlxuICAgKiBAcmV0dXJucyBBbiBhcnJheSBvZiB0b3AgbGV2ZWwgc3RhdGVtZW50cyBmb3IgdGhlIGdpdmVuIG1vZHVsZS5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRNb2R1bGVTdGF0ZW1lbnRzKHNvdXJjZUZpbGU6IHRzLlNvdXJjZUZpbGUpOiB0cy5TdGF0ZW1lbnRbXSB7XG4gICAgcmV0dXJuIEFycmF5LmZyb20oc291cmNlRmlsZS5zdGF0ZW1lbnRzKTtcbiAgfVxuXG4gIC8qKlxuICAgKiBXYWxrIHRoZSBBU1QgbG9va2luZyBmb3IgYW4gYXNzaWdubWVudCB0byB0aGUgc3BlY2lmaWVkIHN5bWJvbC5cbiAgICogQHBhcmFtIG5vZGUgVGhlIGN1cnJlbnQgbm9kZSB3ZSBhcmUgc2VhcmNoaW5nLlxuICAgKiBAcmV0dXJucyBhbiBleHByZXNzaW9uIHRoYXQgcmVwcmVzZW50cyB0aGUgdmFsdWUgb2YgdGhlIHZhcmlhYmxlLCBvciB1bmRlZmluZWQgaWYgbm9uZSBjYW4gYmVcbiAgICogZm91bmQuXG4gICAqL1xuICBwcm90ZWN0ZWQgZmluZERlY29yYXRlZFZhcmlhYmxlVmFsdWUobm9kZTogdHMuTm9kZXx1bmRlZmluZWQsIHN5bWJvbDogdHMuU3ltYm9sKTpcbiAgICAgIHRzLkNhbGxFeHByZXNzaW9ufG51bGwge1xuICAgIGlmICghbm9kZSkge1xuICAgICAgcmV0dXJuIG51bGw7XG4gICAgfVxuICAgIGlmICh0cy5pc0JpbmFyeUV4cHJlc3Npb24obm9kZSkgJiYgbm9kZS5vcGVyYXRvclRva2VuLmtpbmQgPT09IHRzLlN5bnRheEtpbmQuRXF1YWxzVG9rZW4pIHtcbiAgICAgIGNvbnN0IGxlZnQgPSBub2RlLmxlZnQ7XG4gICAgICBjb25zdCByaWdodCA9IG5vZGUucmlnaHQ7XG4gICAgICBpZiAodHMuaXNJZGVudGlmaWVyKGxlZnQpICYmIHRoaXMuY2hlY2tlci5nZXRTeW1ib2xBdExvY2F0aW9uKGxlZnQpID09PSBzeW1ib2wpIHtcbiAgICAgICAgcmV0dXJuICh0cy5pc0NhbGxFeHByZXNzaW9uKHJpZ2h0KSAmJiBnZXRDYWxsZWVOYW1lKHJpZ2h0KSA9PT0gJ19fZGVjb3JhdGUnKSA/IHJpZ2h0IDogbnVsbDtcbiAgICAgIH1cbiAgICAgIHJldHVybiB0aGlzLmZpbmREZWNvcmF0ZWRWYXJpYWJsZVZhbHVlKHJpZ2h0LCBzeW1ib2wpO1xuICAgIH1cbiAgICByZXR1cm4gbm9kZS5mb3JFYWNoQ2hpbGQobm9kZSA9PiB0aGlzLmZpbmREZWNvcmF0ZWRWYXJpYWJsZVZhbHVlKG5vZGUsIHN5bWJvbCkpIHx8IG51bGw7XG4gIH1cblxuICAvKipcbiAgICogVHJ5IHRvIHJldHJpZXZlIHRoZSBzeW1ib2wgb2YgYSBzdGF0aWMgcHJvcGVydHkgb24gYSBjbGFzcy5cbiAgICogQHBhcmFtIHN5bWJvbCB0aGUgY2xhc3Mgd2hvc2UgcHJvcGVydHkgd2UgYXJlIGludGVyZXN0ZWQgaW4uXG4gICAqIEBwYXJhbSBwcm9wZXJ0eU5hbWUgdGhlIG5hbWUgb2Ygc3RhdGljIHByb3BlcnR5LlxuICAgKiBAcmV0dXJucyB0aGUgc3ltYm9sIGlmIGl0IGlzIGZvdW5kIG9yIGB1bmRlZmluZWRgIGlmIG5vdC5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRTdGF0aWNQcm9wZXJ0eShzeW1ib2w6IENsYXNzU3ltYm9sLCBwcm9wZXJ0eU5hbWU6IHRzLl9fU3RyaW5nKTogdHMuU3ltYm9sfHVuZGVmaW5lZCB7XG4gICAgcmV0dXJuIHN5bWJvbC5leHBvcnRzICYmIHN5bWJvbC5leHBvcnRzLmdldChwcm9wZXJ0eU5hbWUpO1xuICB9XG5cbiAgLyoqXG4gICAqIEdldCBhbGwgY2xhc3MgZGVjb3JhdG9ycyBmb3IgdGhlIGdpdmVuIGNsYXNzLCB3aGVyZSB0aGUgZGVjb3JhdG9ycyBhcmUgZGVjbGFyZWRcbiAgICogdmlhIGEgc3RhdGljIHByb3BlcnR5LiBGb3IgZXhhbXBsZTpcbiAgICpcbiAgICogYGBgXG4gICAqIGNsYXNzIFNvbWVEaXJlY3RpdmUge31cbiAgICogU29tZURpcmVjdGl2ZS5kZWNvcmF0b3JzID0gW1xuICAgKiAgIHsgdHlwZTogRGlyZWN0aXZlLCBhcmdzOiBbeyBzZWxlY3RvcjogJ1tzb21lRGlyZWN0aXZlXScgfSxdIH1cbiAgICogXTtcbiAgICogYGBgXG4gICAqXG4gICAqIEBwYXJhbSBkZWNvcmF0b3JzU3ltYm9sIHRoZSBwcm9wZXJ0eSBjb250YWluaW5nIHRoZSBkZWNvcmF0b3JzIHdlIHdhbnQgdG8gZ2V0LlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBkZWNvcmF0b3JzIG9yIG51bGwgaWYgbm9uZSB3aGVyZSBmb3VuZC5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRDbGFzc0RlY29yYXRvcnNGcm9tU3RhdGljUHJvcGVydHkoZGVjb3JhdG9yc1N5bWJvbDogdHMuU3ltYm9sKTogRGVjb3JhdG9yW118bnVsbCB7XG4gICAgY29uc3QgZGVjb3JhdG9yc0lkZW50aWZpZXIgPSBkZWNvcmF0b3JzU3ltYm9sLnZhbHVlRGVjbGFyYXRpb247XG4gICAgaWYgKGRlY29yYXRvcnNJZGVudGlmaWVyICYmIGRlY29yYXRvcnNJZGVudGlmaWVyLnBhcmVudCkge1xuICAgICAgaWYgKHRzLmlzQmluYXJ5RXhwcmVzc2lvbihkZWNvcmF0b3JzSWRlbnRpZmllci5wYXJlbnQpICYmXG4gICAgICAgICAgZGVjb3JhdG9yc0lkZW50aWZpZXIucGFyZW50Lm9wZXJhdG9yVG9rZW4ua2luZCA9PT0gdHMuU3ludGF4S2luZC5FcXVhbHNUb2tlbikge1xuICAgICAgICAvLyBBU1Qgb2YgdGhlIGFycmF5IG9mIGRlY29yYXRvciB2YWx1ZXNcbiAgICAgICAgY29uc3QgZGVjb3JhdG9yc0FycmF5ID0gZGVjb3JhdG9yc0lkZW50aWZpZXIucGFyZW50LnJpZ2h0O1xuICAgICAgICByZXR1cm4gdGhpcy5yZWZsZWN0RGVjb3JhdG9ycyhkZWNvcmF0b3JzQXJyYXkpXG4gICAgICAgICAgICAuZmlsdGVyKGRlY29yYXRvciA9PiB0aGlzLmlzRnJvbUNvcmUoZGVjb3JhdG9yKSk7XG4gICAgICB9XG4gICAgfVxuICAgIHJldHVybiBudWxsO1xuICB9XG5cbiAgLyoqXG4gICAqIEdldCBhbGwgY2xhc3MgZGVjb3JhdG9ycyBmb3IgdGhlIGdpdmVuIGNsYXNzLCB3aGVyZSB0aGUgZGVjb3JhdG9ycyBhcmUgZGVjbGFyZWRcbiAgICogdmlhIHRoZSBgX19kZWNvcmF0ZWAgaGVscGVyIG1ldGhvZC4gRm9yIGV4YW1wbGU6XG4gICAqXG4gICAqIGBgYFxuICAgKiBsZXQgU29tZURpcmVjdGl2ZSA9IGNsYXNzIFNvbWVEaXJlY3RpdmUge31cbiAgICogU29tZURpcmVjdGl2ZSA9IF9fZGVjb3JhdGUoW1xuICAgKiAgIERpcmVjdGl2ZSh7IHNlbGVjdG9yOiAnW3NvbWVEaXJlY3RpdmVdJyB9KSxcbiAgICogXSwgU29tZURpcmVjdGl2ZSk7XG4gICAqIGBgYFxuICAgKlxuICAgKiBAcGFyYW0gc3ltYm9sIHRoZSBjbGFzcyB3aG9zZSBkZWNvcmF0b3JzIHdlIHdhbnQgdG8gZ2V0LlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBkZWNvcmF0b3JzIG9yIG51bGwgaWYgbm9uZSB3aGVyZSBmb3VuZC5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRDbGFzc0RlY29yYXRvcnNGcm9tSGVscGVyQ2FsbChzeW1ib2w6IENsYXNzU3ltYm9sKTogRGVjb3JhdG9yW118bnVsbCB7XG4gICAgY29uc3QgZGVjb3JhdG9yczogRGVjb3JhdG9yW10gPSBbXTtcbiAgICBjb25zdCBoZWxwZXJDYWxscyA9IHRoaXMuZ2V0SGVscGVyQ2FsbHNGb3JDbGFzcyhzeW1ib2wsICdfX2RlY29yYXRlJyk7XG4gICAgaGVscGVyQ2FsbHMuZm9yRWFjaChoZWxwZXJDYWxsID0+IHtcbiAgICAgIGNvbnN0IHtjbGFzc0RlY29yYXRvcnN9ID1cbiAgICAgICAgICB0aGlzLnJlZmxlY3REZWNvcmF0b3JzRnJvbUhlbHBlckNhbGwoaGVscGVyQ2FsbCwgbWFrZUNsYXNzVGFyZ2V0RmlsdGVyKHN5bWJvbC5uYW1lKSk7XG4gICAgICBjbGFzc0RlY29yYXRvcnMuZmlsdGVyKGRlY29yYXRvciA9PiB0aGlzLmlzRnJvbUNvcmUoZGVjb3JhdG9yKSlcbiAgICAgICAgICAuZm9yRWFjaChkZWNvcmF0b3IgPT4gZGVjb3JhdG9ycy5wdXNoKGRlY29yYXRvcikpO1xuICAgIH0pO1xuICAgIHJldHVybiBkZWNvcmF0b3JzLmxlbmd0aCA/IGRlY29yYXRvcnMgOiBudWxsO1xuICB9XG5cbiAgLyoqXG4gICAqIEV4YW1pbmUgYSBzeW1ib2wgd2hpY2ggc2hvdWxkIGJlIG9mIGEgY2xhc3MsIGFuZCByZXR1cm4gbWV0YWRhdGEgYWJvdXQgaXRzIG1lbWJlcnMuXG4gICAqXG4gICAqIEBwYXJhbSBzeW1ib2wgdGhlIGBDbGFzc1N5bWJvbGAgcmVwcmVzZW50aW5nIHRoZSBjbGFzcyBvdmVyIHdoaWNoIHRvIHJlZmxlY3QuXG4gICAqIEByZXR1cm5zIGFuIGFycmF5IG9mIGBDbGFzc01lbWJlcmAgbWV0YWRhdGEgcmVwcmVzZW50aW5nIHRoZSBtZW1iZXJzIG9mIHRoZSBjbGFzcy5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRNZW1iZXJzT2ZTeW1ib2woc3ltYm9sOiBDbGFzc1N5bWJvbCk6IENsYXNzTWVtYmVyW10ge1xuICAgIGNvbnN0IG1lbWJlcnM6IENsYXNzTWVtYmVyW10gPSBbXTtcblxuICAgIC8vIFRoZSBkZWNvcmF0b3JzIG1hcCBjb250YWlucyBhbGwgdGhlIHByb3BlcnRpZXMgdGhhdCBhcmUgZGVjb3JhdGVkXG4gICAgY29uc3QgZGVjb3JhdG9yc01hcCA9IHRoaXMuZ2V0TWVtYmVyRGVjb3JhdG9ycyhzeW1ib2wpO1xuXG4gICAgLy8gVGhlIG1lbWJlciBtYXAgY29udGFpbnMgYWxsIHRoZSBtZXRob2QgKGluc3RhbmNlIGFuZCBzdGF0aWMpOyBhbmQgYW55IGluc3RhbmNlIHByb3BlcnRpZXNcbiAgICAvLyB0aGF0IGFyZSBpbml0aWFsaXplZCBpbiB0aGUgY2xhc3MuXG4gICAgaWYgKHN5bWJvbC5tZW1iZXJzKSB7XG4gICAgICBzeW1ib2wubWVtYmVycy5mb3JFYWNoKCh2YWx1ZSwga2V5KSA9PiB7XG4gICAgICAgIGNvbnN0IGRlY29yYXRvcnMgPSBkZWNvcmF0b3JzTWFwLmdldChrZXkgYXMgc3RyaW5nKTtcbiAgICAgICAgY29uc3QgcmVmbGVjdGVkTWVtYmVycyA9IHRoaXMucmVmbGVjdE1lbWJlcnModmFsdWUsIGRlY29yYXRvcnMpO1xuICAgICAgICBpZiAocmVmbGVjdGVkTWVtYmVycykge1xuICAgICAgICAgIGRlY29yYXRvcnNNYXAuZGVsZXRlKGtleSBhcyBzdHJpbmcpO1xuICAgICAgICAgIG1lbWJlcnMucHVzaCguLi5yZWZsZWN0ZWRNZW1iZXJzKTtcbiAgICAgICAgfVxuICAgICAgfSk7XG4gICAgfVxuXG4gICAgLy8gVGhlIHN0YXRpYyBwcm9wZXJ0eSBtYXAgY29udGFpbnMgYWxsIHRoZSBzdGF0aWMgcHJvcGVydGllc1xuICAgIGlmIChzeW1ib2wuZXhwb3J0cykge1xuICAgICAgc3ltYm9sLmV4cG9ydHMuZm9yRWFjaCgodmFsdWUsIGtleSkgPT4ge1xuICAgICAgICBjb25zdCBkZWNvcmF0b3JzID0gZGVjb3JhdG9yc01hcC5nZXQoa2V5IGFzIHN0cmluZyk7XG4gICAgICAgIGNvbnN0IHJlZmxlY3RlZE1lbWJlcnMgPSB0aGlzLnJlZmxlY3RNZW1iZXJzKHZhbHVlLCBkZWNvcmF0b3JzLCB0cnVlKTtcbiAgICAgICAgaWYgKHJlZmxlY3RlZE1lbWJlcnMpIHtcbiAgICAgICAgICBkZWNvcmF0b3JzTWFwLmRlbGV0ZShrZXkgYXMgc3RyaW5nKTtcbiAgICAgICAgICBtZW1iZXJzLnB1c2goLi4ucmVmbGVjdGVkTWVtYmVycyk7XG4gICAgICAgIH1cbiAgICAgIH0pO1xuICAgIH1cblxuICAgIC8vIElmIHRoaXMgY2xhc3Mgd2FzIGRlY2xhcmVkIGFzIGEgVmFyaWFibGVEZWNsYXJhdGlvbiB0aGVuIGl0IG1heSBoYXZlIHN0YXRpYyBwcm9wZXJ0aWVzXG4gICAgLy8gYXR0YWNoZWQgdG8gdGhlIHZhcmlhYmxlIHJhdGhlciB0aGFuIHRoZSBjbGFzcyBpdHNlbGZcbiAgICAvLyBGb3IgZXhhbXBsZTpcbiAgICAvLyBgYGBcbiAgICAvLyBsZXQgTXlDbGFzcyA9IGNsYXNzIE15Q2xhc3Mge1xuICAgIC8vICAgLy8gbm8gc3RhdGljIHByb3BlcnRpZXMgaGVyZSFcbiAgICAvLyB9XG4gICAgLy8gTXlDbGFzcy5zdGF0aWNQcm9wZXJ0eSA9IC4uLjtcbiAgICAvLyBgYGBcbiAgICBjb25zdCB2YXJpYWJsZURlY2xhcmF0aW9uID0gZ2V0VmFyaWFibGVEZWNsYXJhdGlvbk9mRGVjbGFyYXRpb24oc3ltYm9sLnZhbHVlRGVjbGFyYXRpb24pO1xuICAgIGlmICh2YXJpYWJsZURlY2xhcmF0aW9uICE9PSB1bmRlZmluZWQpIHtcbiAgICAgIGNvbnN0IHZhcmlhYmxlU3ltYm9sID0gdGhpcy5jaGVja2VyLmdldFN5bWJvbEF0TG9jYXRpb24odmFyaWFibGVEZWNsYXJhdGlvbi5uYW1lKTtcbiAgICAgIGlmICh2YXJpYWJsZVN5bWJvbCAmJiB2YXJpYWJsZVN5bWJvbC5leHBvcnRzKSB7XG4gICAgICAgIHZhcmlhYmxlU3ltYm9sLmV4cG9ydHMuZm9yRWFjaCgodmFsdWUsIGtleSkgPT4ge1xuICAgICAgICAgIGNvbnN0IGRlY29yYXRvcnMgPSBkZWNvcmF0b3JzTWFwLmdldChrZXkgYXMgc3RyaW5nKTtcbiAgICAgICAgICBjb25zdCByZWZsZWN0ZWRNZW1iZXJzID0gdGhpcy5yZWZsZWN0TWVtYmVycyh2YWx1ZSwgZGVjb3JhdG9ycywgdHJ1ZSk7XG4gICAgICAgICAgaWYgKHJlZmxlY3RlZE1lbWJlcnMpIHtcbiAgICAgICAgICAgIGRlY29yYXRvcnNNYXAuZGVsZXRlKGtleSBhcyBzdHJpbmcpO1xuICAgICAgICAgICAgbWVtYmVycy5wdXNoKC4uLnJlZmxlY3RlZE1lbWJlcnMpO1xuICAgICAgICAgIH1cbiAgICAgICAgfSk7XG4gICAgICB9XG4gICAgfVxuXG4gICAgLy8gRGVhbCB3aXRoIGFueSBkZWNvcmF0ZWQgcHJvcGVydGllcyB0aGF0IHdlcmUgbm90IGluaXRpYWxpemVkIGluIHRoZSBjbGFzc1xuICAgIGRlY29yYXRvcnNNYXAuZm9yRWFjaCgodmFsdWUsIGtleSkgPT4ge1xuICAgICAgbWVtYmVycy5wdXNoKHtcbiAgICAgICAgaW1wbGVtZW50YXRpb246IG51bGwsXG4gICAgICAgIGRlY29yYXRvcnM6IHZhbHVlLFxuICAgICAgICBpc1N0YXRpYzogZmFsc2UsXG4gICAgICAgIGtpbmQ6IENsYXNzTWVtYmVyS2luZC5Qcm9wZXJ0eSxcbiAgICAgICAgbmFtZToga2V5LFxuICAgICAgICBuYW1lTm9kZTogbnVsbCxcbiAgICAgICAgbm9kZTogbnVsbCxcbiAgICAgICAgdHlwZTogbnVsbCxcbiAgICAgICAgdmFsdWU6IG51bGxcbiAgICAgIH0pO1xuICAgIH0pO1xuXG4gICAgcmV0dXJuIG1lbWJlcnM7XG4gIH1cblxuICAvKipcbiAgICogR2V0IGFsbCB0aGUgbWVtYmVyIGRlY29yYXRvcnMgZm9yIHRoZSBnaXZlbiBjbGFzcy5cbiAgICogQHBhcmFtIGNsYXNzU3ltYm9sIHRoZSBjbGFzcyB3aG9zZSBtZW1iZXIgZGVjb3JhdG9ycyB3ZSBhcmUgaW50ZXJlc3RlZCBpbi5cbiAgICogQHJldHVybnMgYSBtYXAgd2hvc2Uga2V5cyBhcmUgdGhlIG5hbWUgb2YgdGhlIG1lbWJlcnMgYW5kIHdob3NlIHZhbHVlcyBhcmUgY29sbGVjdGlvbnMgb2ZcbiAgICogZGVjb3JhdG9ycyBmb3IgdGhlIGdpdmVuIG1lbWJlci5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRNZW1iZXJEZWNvcmF0b3JzKGNsYXNzU3ltYm9sOiBDbGFzc1N5bWJvbCk6IE1hcDxzdHJpbmcsIERlY29yYXRvcltdPiB7XG4gICAgY29uc3QgZGVjb3JhdG9yc1Byb3BlcnR5ID0gdGhpcy5nZXRTdGF0aWNQcm9wZXJ0eShjbGFzc1N5bWJvbCwgUFJPUF9ERUNPUkFUT1JTKTtcbiAgICBpZiAoZGVjb3JhdG9yc1Byb3BlcnR5KSB7XG4gICAgICByZXR1cm4gdGhpcy5nZXRNZW1iZXJEZWNvcmF0b3JzRnJvbVN0YXRpY1Byb3BlcnR5KGRlY29yYXRvcnNQcm9wZXJ0eSk7XG4gICAgfSBlbHNlIHtcbiAgICAgIHJldHVybiB0aGlzLmdldE1lbWJlckRlY29yYXRvcnNGcm9tSGVscGVyQ2FsbHMoY2xhc3NTeW1ib2wpO1xuICAgIH1cbiAgfVxuXG4gIC8qKlxuICAgKiBNZW1iZXIgZGVjb3JhdG9ycyBtYXkgYmUgZGVjbGFyZWQgYXMgc3RhdGljIHByb3BlcnRpZXMgb2YgdGhlIGNsYXNzOlxuICAgKlxuICAgKiBgYGBcbiAgICogU29tZURpcmVjdGl2ZS5wcm9wRGVjb3JhdG9ycyA9IHtcbiAgICogICBcIm5nRm9yT2ZcIjogW3sgdHlwZTogSW5wdXQgfSxdLFxuICAgKiAgIFwibmdGb3JUcmFja0J5XCI6IFt7IHR5cGU6IElucHV0IH0sXSxcbiAgICogICBcIm5nRm9yVGVtcGxhdGVcIjogW3sgdHlwZTogSW5wdXQgfSxdLFxuICAgKiB9O1xuICAgKiBgYGBcbiAgICpcbiAgICogQHBhcmFtIGRlY29yYXRvcnNQcm9wZXJ0eSB0aGUgY2xhc3Mgd2hvc2UgbWVtYmVyIGRlY29yYXRvcnMgd2UgYXJlIGludGVyZXN0ZWQgaW4uXG4gICAqIEByZXR1cm5zIGEgbWFwIHdob3NlIGtleXMgYXJlIHRoZSBuYW1lIG9mIHRoZSBtZW1iZXJzIGFuZCB3aG9zZSB2YWx1ZXMgYXJlIGNvbGxlY3Rpb25zIG9mXG4gICAqIGRlY29yYXRvcnMgZm9yIHRoZSBnaXZlbiBtZW1iZXIuXG4gICAqL1xuICBwcm90ZWN0ZWQgZ2V0TWVtYmVyRGVjb3JhdG9yc0Zyb21TdGF0aWNQcm9wZXJ0eShkZWNvcmF0b3JzUHJvcGVydHk6IHRzLlN5bWJvbCk6XG4gICAgICBNYXA8c3RyaW5nLCBEZWNvcmF0b3JbXT4ge1xuICAgIGNvbnN0IG1lbWJlckRlY29yYXRvcnMgPSBuZXcgTWFwPHN0cmluZywgRGVjb3JhdG9yW10+KCk7XG4gICAgLy8gU3ltYm9sIG9mIHRoZSBpZGVudGlmaWVyIGZvciBgU29tZURpcmVjdGl2ZS5wcm9wRGVjb3JhdG9yc2AuXG4gICAgY29uc3QgcHJvcERlY29yYXRvcnNNYXAgPSBnZXRQcm9wZXJ0eVZhbHVlRnJvbVN5bWJvbChkZWNvcmF0b3JzUHJvcGVydHkpO1xuICAgIGlmIChwcm9wRGVjb3JhdG9yc01hcCAmJiB0cy5pc09iamVjdExpdGVyYWxFeHByZXNzaW9uKHByb3BEZWNvcmF0b3JzTWFwKSkge1xuICAgICAgY29uc3QgcHJvcGVydGllc01hcCA9IHJlZmxlY3RPYmplY3RMaXRlcmFsKHByb3BEZWNvcmF0b3JzTWFwKTtcbiAgICAgIHByb3BlcnRpZXNNYXAuZm9yRWFjaCgodmFsdWUsIG5hbWUpID0+IHtcbiAgICAgICAgY29uc3QgZGVjb3JhdG9ycyA9XG4gICAgICAgICAgICB0aGlzLnJlZmxlY3REZWNvcmF0b3JzKHZhbHVlKS5maWx0ZXIoZGVjb3JhdG9yID0+IHRoaXMuaXNGcm9tQ29yZShkZWNvcmF0b3IpKTtcbiAgICAgICAgaWYgKGRlY29yYXRvcnMubGVuZ3RoKSB7XG4gICAgICAgICAgbWVtYmVyRGVjb3JhdG9ycy5zZXQobmFtZSwgZGVjb3JhdG9ycyk7XG4gICAgICAgIH1cbiAgICAgIH0pO1xuICAgIH1cbiAgICByZXR1cm4gbWVtYmVyRGVjb3JhdG9ycztcbiAgfVxuXG4gIC8qKlxuICAgKiBNZW1iZXIgZGVjb3JhdG9ycyBtYXkgYmUgZGVjbGFyZWQgdmlhIGhlbHBlciBjYWxsIHN0YXRlbWVudHMuXG4gICAqXG4gICAqIGBgYFxuICAgKiBfX2RlY29yYXRlKFtcbiAgICogICAgIElucHV0KCksXG4gICAqICAgICBfX21ldGFkYXRhKFwiZGVzaWduOnR5cGVcIiwgU3RyaW5nKVxuICAgKiBdLCBTb21lRGlyZWN0aXZlLnByb3RvdHlwZSwgXCJpbnB1dDFcIiwgdm9pZCAwKTtcbiAgICogYGBgXG4gICAqXG4gICAqIEBwYXJhbSBjbGFzc1N5bWJvbCB0aGUgY2xhc3Mgd2hvc2UgbWVtYmVyIGRlY29yYXRvcnMgd2UgYXJlIGludGVyZXN0ZWQgaW4uXG4gICAqIEByZXR1cm5zIGEgbWFwIHdob3NlIGtleXMgYXJlIHRoZSBuYW1lIG9mIHRoZSBtZW1iZXJzIGFuZCB3aG9zZSB2YWx1ZXMgYXJlIGNvbGxlY3Rpb25zIG9mXG4gICAqIGRlY29yYXRvcnMgZm9yIHRoZSBnaXZlbiBtZW1iZXIuXG4gICAqL1xuICBwcm90ZWN0ZWQgZ2V0TWVtYmVyRGVjb3JhdG9yc0Zyb21IZWxwZXJDYWxscyhjbGFzc1N5bWJvbDogQ2xhc3NTeW1ib2wpOiBNYXA8c3RyaW5nLCBEZWNvcmF0b3JbXT4ge1xuICAgIGNvbnN0IG1lbWJlckRlY29yYXRvck1hcCA9IG5ldyBNYXA8c3RyaW5nLCBEZWNvcmF0b3JbXT4oKTtcbiAgICBjb25zdCBoZWxwZXJDYWxscyA9IHRoaXMuZ2V0SGVscGVyQ2FsbHNGb3JDbGFzcyhjbGFzc1N5bWJvbCwgJ19fZGVjb3JhdGUnKTtcbiAgICBoZWxwZXJDYWxscy5mb3JFYWNoKGhlbHBlckNhbGwgPT4ge1xuICAgICAgY29uc3Qge21lbWJlckRlY29yYXRvcnN9ID0gdGhpcy5yZWZsZWN0RGVjb3JhdG9yc0Zyb21IZWxwZXJDYWxsKFxuICAgICAgICAgIGhlbHBlckNhbGwsIG1ha2VNZW1iZXJUYXJnZXRGaWx0ZXIoY2xhc3NTeW1ib2wubmFtZSkpO1xuICAgICAgbWVtYmVyRGVjb3JhdG9ycy5mb3JFYWNoKChkZWNvcmF0b3JzLCBtZW1iZXJOYW1lKSA9PiB7XG4gICAgICAgIGlmIChtZW1iZXJOYW1lKSB7XG4gICAgICAgICAgY29uc3QgbWVtYmVyRGVjb3JhdG9ycyA9XG4gICAgICAgICAgICAgIG1lbWJlckRlY29yYXRvck1hcC5oYXMobWVtYmVyTmFtZSkgPyBtZW1iZXJEZWNvcmF0b3JNYXAuZ2V0KG1lbWJlck5hbWUpICEgOiBbXTtcbiAgICAgICAgICBjb25zdCBjb3JlRGVjb3JhdG9ycyA9IGRlY29yYXRvcnMuZmlsdGVyKGRlY29yYXRvciA9PiB0aGlzLmlzRnJvbUNvcmUoZGVjb3JhdG9yKSk7XG4gICAgICAgICAgbWVtYmVyRGVjb3JhdG9yTWFwLnNldChtZW1iZXJOYW1lLCBtZW1iZXJEZWNvcmF0b3JzLmNvbmNhdChjb3JlRGVjb3JhdG9ycykpO1xuICAgICAgICB9XG4gICAgICB9KTtcbiAgICB9KTtcbiAgICByZXR1cm4gbWVtYmVyRGVjb3JhdG9yTWFwO1xuICB9XG5cbiAgLyoqXG4gICAqIEV4dHJhY3QgZGVjb3JhdG9yIGluZm8gZnJvbSBgX19kZWNvcmF0ZWAgaGVscGVyIGZ1bmN0aW9uIGNhbGxzLlxuICAgKiBAcGFyYW0gaGVscGVyQ2FsbCB0aGUgY2FsbCB0byBhIGhlbHBlciB0aGF0IG1heSBjb250YWluIGRlY29yYXRvciBjYWxsc1xuICAgKiBAcGFyYW0gdGFyZ2V0RmlsdGVyIGEgZnVuY3Rpb24gdG8gZmlsdGVyIG91dCB0YXJnZXRzIHRoYXQgd2UgYXJlIG5vdCBpbnRlcmVzdGVkIGluLlxuICAgKiBAcmV0dXJucyBhIG1hcHBpbmcgZnJvbSBtZW1iZXIgbmFtZSB0byBkZWNvcmF0b3JzLCB3aGVyZSB0aGUga2V5IGlzIGVpdGhlciB0aGUgbmFtZSBvZiB0aGVcbiAgICogbWVtYmVyIG9yIGB1bmRlZmluZWRgIGlmIGl0IHJlZmVycyB0byBkZWNvcmF0b3JzIG9uIHRoZSBjbGFzcyBhcyBhIHdob2xlLlxuICAgKi9cbiAgcHJvdGVjdGVkIHJlZmxlY3REZWNvcmF0b3JzRnJvbUhlbHBlckNhbGwoXG4gICAgICBoZWxwZXJDYWxsOiB0cy5DYWxsRXhwcmVzc2lvbiwgdGFyZ2V0RmlsdGVyOiBUYXJnZXRGaWx0ZXIpOlxuICAgICAge2NsYXNzRGVjb3JhdG9yczogRGVjb3JhdG9yW10sIG1lbWJlckRlY29yYXRvcnM6IE1hcDxzdHJpbmcsIERlY29yYXRvcltdPn0ge1xuICAgIGNvbnN0IGNsYXNzRGVjb3JhdG9yczogRGVjb3JhdG9yW10gPSBbXTtcbiAgICBjb25zdCBtZW1iZXJEZWNvcmF0b3JzID0gbmV3IE1hcDxzdHJpbmcsIERlY29yYXRvcltdPigpO1xuXG4gICAgLy8gRmlyc3QgY2hlY2sgdGhhdCB0aGUgYHRhcmdldGAgYXJndW1lbnQgaXMgY29ycmVjdFxuICAgIGlmICh0YXJnZXRGaWx0ZXIoaGVscGVyQ2FsbC5hcmd1bWVudHNbMV0pKSB7XG4gICAgICAvLyBHcmFiIHRoZSBgZGVjb3JhdG9yc2AgYXJndW1lbnQgd2hpY2ggc2hvdWxkIGJlIGFuIGFycmF5IG9mIGNhbGxzXG4gICAgICBjb25zdCBkZWNvcmF0b3JDYWxscyA9IGhlbHBlckNhbGwuYXJndW1lbnRzWzBdO1xuICAgICAgaWYgKGRlY29yYXRvckNhbGxzICYmIHRzLmlzQXJyYXlMaXRlcmFsRXhwcmVzc2lvbihkZWNvcmF0b3JDYWxscykpIHtcbiAgICAgICAgZGVjb3JhdG9yQ2FsbHMuZWxlbWVudHMuZm9yRWFjaChlbGVtZW50ID0+IHtcbiAgICAgICAgICAvLyBXZSBvbmx5IGNhcmUgYWJvdXQgdGhvc2UgZWxlbWVudHMgdGhhdCBhcmUgYWN0dWFsIGNhbGxzXG4gICAgICAgICAgaWYgKHRzLmlzQ2FsbEV4cHJlc3Npb24oZWxlbWVudCkpIHtcbiAgICAgICAgICAgIGNvbnN0IGRlY29yYXRvciA9IHRoaXMucmVmbGVjdERlY29yYXRvckNhbGwoZWxlbWVudCk7XG4gICAgICAgICAgICBpZiAoZGVjb3JhdG9yKSB7XG4gICAgICAgICAgICAgIGNvbnN0IGtleUFyZyA9IGhlbHBlckNhbGwuYXJndW1lbnRzWzJdO1xuICAgICAgICAgICAgICBjb25zdCBrZXlOYW1lID0ga2V5QXJnICYmIHRzLmlzU3RyaW5nTGl0ZXJhbChrZXlBcmcpID8ga2V5QXJnLnRleHQgOiB1bmRlZmluZWQ7XG4gICAgICAgICAgICAgIGlmIChrZXlOYW1lID09PSB1bmRlZmluZWQpIHtcbiAgICAgICAgICAgICAgICBjbGFzc0RlY29yYXRvcnMucHVzaChkZWNvcmF0b3IpO1xuICAgICAgICAgICAgICB9IGVsc2Uge1xuICAgICAgICAgICAgICAgIGNvbnN0IGRlY29yYXRvcnMgPVxuICAgICAgICAgICAgICAgICAgICBtZW1iZXJEZWNvcmF0b3JzLmhhcyhrZXlOYW1lKSA/IG1lbWJlckRlY29yYXRvcnMuZ2V0KGtleU5hbWUpICEgOiBbXTtcbiAgICAgICAgICAgICAgICBkZWNvcmF0b3JzLnB1c2goZGVjb3JhdG9yKTtcbiAgICAgICAgICAgICAgICBtZW1iZXJEZWNvcmF0b3JzLnNldChrZXlOYW1lLCBkZWNvcmF0b3JzKTtcbiAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgfVxuICAgICAgICAgIH1cbiAgICAgICAgfSk7XG4gICAgICB9XG4gICAgfVxuICAgIHJldHVybiB7Y2xhc3NEZWNvcmF0b3JzLCBtZW1iZXJEZWNvcmF0b3JzfTtcbiAgfVxuXG4gIC8qKlxuICAgKiBFeHRyYWN0IHRoZSBkZWNvcmF0b3IgaW5mb3JtYXRpb24gZnJvbSBhIGNhbGwgdG8gYSBkZWNvcmF0b3IgYXMgYSBmdW5jdGlvbi5cbiAgICogVGhpcyBoYXBwZW5zIHdoZW4gdGhlIGRlY29yYXRvcnMgaGFzIGJlZW4gdXNlZCBpbiBhIGBfX2RlY29yYXRlYCBoZWxwZXIgY2FsbC5cbiAgICogRm9yIGV4YW1wbGU6XG4gICAqXG4gICAqIGBgYFxuICAgKiBfX2RlY29yYXRlKFtcbiAgICogICBEaXJlY3RpdmUoeyBzZWxlY3RvcjogJ1tzb21lRGlyZWN0aXZlXScgfSksXG4gICAqIF0sIFNvbWVEaXJlY3RpdmUpO1xuICAgKiBgYGBcbiAgICpcbiAgICogSGVyZSB0aGUgYERpcmVjdGl2ZWAgZGVjb3JhdG9yIGlzIGRlY29yYXRpbmcgYFNvbWVEaXJlY3RpdmVgIGFuZCB0aGUgb3B0aW9ucyBmb3JcbiAgICogdGhlIGRlY29yYXRvciBhcmUgcGFzc2VkIGFzIGFyZ3VtZW50cyB0byB0aGUgYERpcmVjdGl2ZSgpYCBjYWxsLlxuICAgKlxuICAgKiBAcGFyYW0gY2FsbCB0aGUgY2FsbCB0byB0aGUgZGVjb3JhdG9yLlxuICAgKiBAcmV0dXJucyBhIGRlY29yYXRvciBjb250YWluaW5nIHRoZSByZWZsZWN0ZWQgaW5mb3JtYXRpb24sIG9yIG51bGwgaWYgdGhlIGNhbGxcbiAgICogaXMgbm90IGEgdmFsaWQgZGVjb3JhdG9yIGNhbGwuXG4gICAqL1xuICBwcm90ZWN0ZWQgcmVmbGVjdERlY29yYXRvckNhbGwoY2FsbDogdHMuQ2FsbEV4cHJlc3Npb24pOiBEZWNvcmF0b3J8bnVsbCB7XG4gICAgY29uc3QgZGVjb3JhdG9yRXhwcmVzc2lvbiA9IGNhbGwuZXhwcmVzc2lvbjtcbiAgICBpZiAoaXNEZWNvcmF0b3JJZGVudGlmaWVyKGRlY29yYXRvckV4cHJlc3Npb24pKSB7XG4gICAgICAvLyBXZSBmb3VuZCBhIGRlY29yYXRvciFcbiAgICAgIGNvbnN0IGRlY29yYXRvcklkZW50aWZpZXIgPVxuICAgICAgICAgIHRzLmlzSWRlbnRpZmllcihkZWNvcmF0b3JFeHByZXNzaW9uKSA/IGRlY29yYXRvckV4cHJlc3Npb24gOiBkZWNvcmF0b3JFeHByZXNzaW9uLm5hbWU7XG4gICAgICByZXR1cm4ge1xuICAgICAgICBuYW1lOiBkZWNvcmF0b3JJZGVudGlmaWVyLnRleHQsXG4gICAgICAgIGlkZW50aWZpZXI6IGRlY29yYXRvcklkZW50aWZpZXIsXG4gICAgICAgIGltcG9ydDogdGhpcy5nZXRJbXBvcnRPZklkZW50aWZpZXIoZGVjb3JhdG9ySWRlbnRpZmllciksXG4gICAgICAgIG5vZGU6IGNhbGwsXG4gICAgICAgIGFyZ3M6IEFycmF5LmZyb20oY2FsbC5hcmd1bWVudHMpXG4gICAgICB9O1xuICAgIH1cbiAgICByZXR1cm4gbnVsbDtcbiAgfVxuXG4gIC8qKlxuICAgKiBDaGVjayB0aGUgZ2l2ZW4gc3RhdGVtZW50IHRvIHNlZSBpZiBpdCBpcyBhIGNhbGwgdG8gdGhlIHNwZWNpZmllZCBoZWxwZXIgZnVuY3Rpb24gb3IgbnVsbCBpZlxuICAgKiBub3QgZm91bmQuXG4gICAqXG4gICAqIE1hdGNoaW5nIHN0YXRlbWVudHMgd2lsbCBsb29rIGxpa2U6ICBgdHNsaWJfMS5fX2RlY29yYXRlKC4uLik7YC5cbiAgICogQHBhcmFtIHN0YXRlbWVudCB0aGUgc3RhdGVtZW50IHRoYXQgbWF5IGNvbnRhaW4gdGhlIGNhbGwuXG4gICAqIEBwYXJhbSBoZWxwZXJOYW1lIHRoZSBuYW1lIG9mIHRoZSBoZWxwZXIgd2UgYXJlIGxvb2tpbmcgZm9yLlxuICAgKiBAcmV0dXJucyB0aGUgbm9kZSB0aGF0IGNvcnJlc3BvbmRzIHRvIHRoZSBgX19kZWNvcmF0ZSguLi4pYCBjYWxsIG9yIG51bGwgaWYgdGhlIHN0YXRlbWVudFxuICAgKiBkb2VzIG5vdCBtYXRjaC5cbiAgICovXG4gIHByb3RlY3RlZCBnZXRIZWxwZXJDYWxsKHN0YXRlbWVudDogdHMuU3RhdGVtZW50LCBoZWxwZXJOYW1lOiBzdHJpbmcpOiB0cy5DYWxsRXhwcmVzc2lvbnxudWxsIHtcbiAgICBpZiAodHMuaXNFeHByZXNzaW9uU3RhdGVtZW50KHN0YXRlbWVudCkpIHtcbiAgICAgIGxldCBleHByZXNzaW9uID0gc3RhdGVtZW50LmV4cHJlc3Npb247XG4gICAgICB3aGlsZSAoaXNBc3NpZ25tZW50KGV4cHJlc3Npb24pKSB7XG4gICAgICAgIGV4cHJlc3Npb24gPSBleHByZXNzaW9uLnJpZ2h0O1xuICAgICAgfVxuICAgICAgaWYgKHRzLmlzQ2FsbEV4cHJlc3Npb24oZXhwcmVzc2lvbikgJiYgZ2V0Q2FsbGVlTmFtZShleHByZXNzaW9uKSA9PT0gaGVscGVyTmFtZSkge1xuICAgICAgICByZXR1cm4gZXhwcmVzc2lvbjtcbiAgICAgIH1cbiAgICB9XG4gICAgcmV0dXJuIG51bGw7XG4gIH1cblxuXG4gIC8qKlxuICAgKiBSZWZsZWN0IG92ZXIgdGhlIGdpdmVuIGFycmF5IG5vZGUgYW5kIGV4dHJhY3QgZGVjb3JhdG9yIGluZm9ybWF0aW9uIGZyb20gZWFjaCBlbGVtZW50LlxuICAgKlxuICAgKiBUaGlzIGlzIHVzZWQgZm9yIGRlY29yYXRvcnMgdGhhdCBhcmUgZGVmaW5lZCBpbiBzdGF0aWMgcHJvcGVydGllcy4gRm9yIGV4YW1wbGU6XG4gICAqXG4gICAqIGBgYFxuICAgKiBTb21lRGlyZWN0aXZlLmRlY29yYXRvcnMgPSBbXG4gICAqICAgeyB0eXBlOiBEaXJlY3RpdmUsIGFyZ3M6IFt7IHNlbGVjdG9yOiAnW3NvbWVEaXJlY3RpdmVdJyB9LF0gfVxuICAgKiBdO1xuICAgKiBgYGBcbiAgICpcbiAgICogQHBhcmFtIGRlY29yYXRvcnNBcnJheSBhbiBleHByZXNzaW9uIHRoYXQgY29udGFpbnMgZGVjb3JhdG9yIGluZm9ybWF0aW9uLlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBkZWNvcmF0b3IgaW5mbyB0aGF0IHdhcyByZWZsZWN0ZWQgZnJvbSB0aGUgYXJyYXkgbm9kZS5cbiAgICovXG4gIHByb3RlY3RlZCByZWZsZWN0RGVjb3JhdG9ycyhkZWNvcmF0b3JzQXJyYXk6IHRzLkV4cHJlc3Npb24pOiBEZWNvcmF0b3JbXSB7XG4gICAgY29uc3QgZGVjb3JhdG9yczogRGVjb3JhdG9yW10gPSBbXTtcblxuICAgIGlmICh0cy5pc0FycmF5TGl0ZXJhbEV4cHJlc3Npb24oZGVjb3JhdG9yc0FycmF5KSkge1xuICAgICAgLy8gQWRkIGVhY2ggZGVjb3JhdG9yIHRoYXQgaXMgaW1wb3J0ZWQgZnJvbSBgQGFuZ3VsYXIvY29yZWAgaW50byB0aGUgYGRlY29yYXRvcnNgIGFycmF5XG4gICAgICBkZWNvcmF0b3JzQXJyYXkuZWxlbWVudHMuZm9yRWFjaChub2RlID0+IHtcblxuICAgICAgICAvLyBJZiB0aGUgZGVjb3JhdG9yIGlzIG5vdCBhbiBvYmplY3QgbGl0ZXJhbCBleHByZXNzaW9uIHRoZW4gd2UgYXJlIG5vdCBpbnRlcmVzdGVkXG4gICAgICAgIGlmICh0cy5pc09iamVjdExpdGVyYWxFeHByZXNzaW9uKG5vZGUpKSB7XG4gICAgICAgICAgLy8gV2UgYXJlIG9ubHkgaW50ZXJlc3RlZCBpbiBvYmplY3RzIG9mIHRoZSBmb3JtOiBgeyB0eXBlOiBEZWNvcmF0b3JUeXBlLCBhcmdzOiBbLi4uXSB9YFxuICAgICAgICAgIGNvbnN0IGRlY29yYXRvciA9IHJlZmxlY3RPYmplY3RMaXRlcmFsKG5vZGUpO1xuXG4gICAgICAgICAgLy8gSXMgdGhlIHZhbHVlIG9mIHRoZSBgdHlwZWAgcHJvcGVydHkgYW4gaWRlbnRpZmllcj9cbiAgICAgICAgICBpZiAoZGVjb3JhdG9yLmhhcygndHlwZScpKSB7XG4gICAgICAgICAgICBsZXQgZGVjb3JhdG9yVHlwZSA9IGRlY29yYXRvci5nZXQoJ3R5cGUnKSAhO1xuICAgICAgICAgICAgaWYgKGlzRGVjb3JhdG9ySWRlbnRpZmllcihkZWNvcmF0b3JUeXBlKSkge1xuICAgICAgICAgICAgICBjb25zdCBkZWNvcmF0b3JJZGVudGlmaWVyID1cbiAgICAgICAgICAgICAgICAgIHRzLmlzSWRlbnRpZmllcihkZWNvcmF0b3JUeXBlKSA/IGRlY29yYXRvclR5cGUgOiBkZWNvcmF0b3JUeXBlLm5hbWU7XG4gICAgICAgICAgICAgIGRlY29yYXRvcnMucHVzaCh7XG4gICAgICAgICAgICAgICAgbmFtZTogZGVjb3JhdG9ySWRlbnRpZmllci50ZXh0LFxuICAgICAgICAgICAgICAgIGlkZW50aWZpZXI6IGRlY29yYXRvclR5cGUsXG4gICAgICAgICAgICAgICAgaW1wb3J0OiB0aGlzLmdldEltcG9ydE9mSWRlbnRpZmllcihkZWNvcmF0b3JJZGVudGlmaWVyKSwgbm9kZSxcbiAgICAgICAgICAgICAgICBhcmdzOiBnZXREZWNvcmF0b3JBcmdzKG5vZGUpLFxuICAgICAgICAgICAgICB9KTtcbiAgICAgICAgICAgIH1cbiAgICAgICAgICB9XG4gICAgICAgIH1cbiAgICAgIH0pO1xuICAgIH1cbiAgICByZXR1cm4gZGVjb3JhdG9ycztcbiAgfVxuXG4gIC8qKlxuICAgKiBSZWZsZWN0IG92ZXIgYSBzeW1ib2wgYW5kIGV4dHJhY3QgdGhlIG1lbWJlciBpbmZvcm1hdGlvbiwgY29tYmluaW5nIGl0IHdpdGggdGhlXG4gICAqIHByb3ZpZGVkIGRlY29yYXRvciBpbmZvcm1hdGlvbiwgYW5kIHdoZXRoZXIgaXQgaXMgYSBzdGF0aWMgbWVtYmVyLlxuICAgKlxuICAgKiBBIHNpbmdsZSBzeW1ib2wgbWF5IHJlcHJlc2VudCBtdWx0aXBsZSBjbGFzcyBtZW1iZXJzIGluIHRoZSBjYXNlIG9mIGFjY2Vzc29ycztcbiAgICogYW4gZXF1YWxseSBuYW1lZCBnZXR0ZXIvc2V0dGVyIGFjY2Vzc29yIHBhaXIgaXMgY29tYmluZWQgaW50byBhIHNpbmdsZSBzeW1ib2wuXG4gICAqIFdoZW4gdGhlIHN5bWJvbCBpcyByZWNvZ25pemVkIGFzIHJlcHJlc2VudGluZyBhbiBhY2Nlc3NvciwgaXRzIGRlY2xhcmF0aW9ucyBhcmVcbiAgICogYW5hbHl6ZWQgc3VjaCB0aGF0IGJvdGggdGhlIHNldHRlciBhbmQgZ2V0dGVyIGFjY2Vzc29yIGFyZSByZXR1cm5lZCBhcyBzZXBhcmF0ZVxuICAgKiBjbGFzcyBtZW1iZXJzLlxuICAgKlxuICAgKiBPbmUgZGlmZmVyZW5jZSB3cnQgdGhlIFR5cGVTY3JpcHQgaG9zdCBpcyB0aGF0IGluIEVTMjAxNSwgd2UgY2Fubm90IHNlZSB3aGljaFxuICAgKiBhY2Nlc3NvciBvcmlnaW5hbGx5IGhhZCBhbnkgZGVjb3JhdG9ycyBhcHBsaWVkIHRvIHRoZW0sIGFzIGRlY29yYXRvcnMgYXJlIGFwcGxpZWRcbiAgICogdG8gdGhlIHByb3BlcnR5IGRlc2NyaXB0b3IgaW4gZ2VuZXJhbCwgbm90IGEgc3BlY2lmaWMgYWNjZXNzb3IuIElmIGFuIGFjY2Vzc29yXG4gICAqIGhhcyBib3RoIGEgc2V0dGVyIGFuZCBnZXR0ZXIsIGFueSBkZWNvcmF0b3JzIGFyZSBvbmx5IGF0dGFjaGVkIHRvIHRoZSBzZXR0ZXIgbWVtYmVyLlxuICAgKlxuICAgKiBAcGFyYW0gc3ltYm9sIHRoZSBzeW1ib2wgZm9yIHRoZSBtZW1iZXIgdG8gcmVmbGVjdCBvdmVyLlxuICAgKiBAcGFyYW0gZGVjb3JhdG9ycyBhbiBhcnJheSBvZiBkZWNvcmF0b3JzIGFzc29jaWF0ZWQgd2l0aCB0aGUgbWVtYmVyLlxuICAgKiBAcGFyYW0gaXNTdGF0aWMgdHJ1ZSBpZiB0aGlzIG1lbWJlciBpcyBzdGF0aWMsIGZhbHNlIGlmIGl0IGlzIGFuIGluc3RhbmNlIHByb3BlcnR5LlxuICAgKiBAcmV0dXJucyB0aGUgcmVmbGVjdGVkIG1lbWJlciBpbmZvcm1hdGlvbiwgb3IgbnVsbCBpZiB0aGUgc3ltYm9sIGlzIG5vdCBhIG1lbWJlci5cbiAgICovXG4gIHByb3RlY3RlZCByZWZsZWN0TWVtYmVycyhzeW1ib2w6IHRzLlN5bWJvbCwgZGVjb3JhdG9ycz86IERlY29yYXRvcltdLCBpc1N0YXRpYz86IGJvb2xlYW4pOlxuICAgICAgQ2xhc3NNZW1iZXJbXXxudWxsIHtcbiAgICBpZiAoc3ltYm9sLmZsYWdzICYgdHMuU3ltYm9sRmxhZ3MuQWNjZXNzb3IpIHtcbiAgICAgIGNvbnN0IG1lbWJlcnM6IENsYXNzTWVtYmVyW10gPSBbXTtcbiAgICAgIGNvbnN0IHNldHRlciA9IHN5bWJvbC5kZWNsYXJhdGlvbnMgJiYgc3ltYm9sLmRlY2xhcmF0aW9ucy5maW5kKHRzLmlzU2V0QWNjZXNzb3IpO1xuICAgICAgY29uc3QgZ2V0dGVyID0gc3ltYm9sLmRlY2xhcmF0aW9ucyAmJiBzeW1ib2wuZGVjbGFyYXRpb25zLmZpbmQodHMuaXNHZXRBY2Nlc3Nvcik7XG5cbiAgICAgIGNvbnN0IHNldHRlck1lbWJlciA9XG4gICAgICAgICAgc2V0dGVyICYmIHRoaXMucmVmbGVjdE1lbWJlcihzZXR0ZXIsIENsYXNzTWVtYmVyS2luZC5TZXR0ZXIsIGRlY29yYXRvcnMsIGlzU3RhdGljKTtcbiAgICAgIGlmIChzZXR0ZXJNZW1iZXIpIHtcbiAgICAgICAgbWVtYmVycy5wdXNoKHNldHRlck1lbWJlcik7XG5cbiAgICAgICAgLy8gUHJldmVudCBhdHRhY2hpbmcgdGhlIGRlY29yYXRvcnMgdG8gYSBwb3RlbnRpYWwgZ2V0dGVyLiBJbiBFUzIwMTUsIHdlIGNhbid0IHRlbGwgd2hlcmVcbiAgICAgICAgLy8gdGhlIGRlY29yYXRvcnMgd2VyZSBvcmlnaW5hbGx5IGF0dGFjaGVkIHRvLCBob3dldmVyIHdlIG9ubHkgd2FudCB0byBhdHRhY2ggdGhlbSB0byBhXG4gICAgICAgIC8vIHNpbmdsZSBgQ2xhc3NNZW1iZXJgIGFzIG90aGVyd2lzZSBuZ3RzYyB3b3VsZCBoYW5kbGUgdGhlIHNhbWUgZGVjb3JhdG9ycyB0d2ljZS5cbiAgICAgICAgZGVjb3JhdG9ycyA9IHVuZGVmaW5lZDtcbiAgICAgIH1cblxuICAgICAgY29uc3QgZ2V0dGVyTWVtYmVyID1cbiAgICAgICAgICBnZXR0ZXIgJiYgdGhpcy5yZWZsZWN0TWVtYmVyKGdldHRlciwgQ2xhc3NNZW1iZXJLaW5kLkdldHRlciwgZGVjb3JhdG9ycywgaXNTdGF0aWMpO1xuICAgICAgaWYgKGdldHRlck1lbWJlcikge1xuICAgICAgICBtZW1iZXJzLnB1c2goZ2V0dGVyTWVtYmVyKTtcbiAgICAgIH1cblxuICAgICAgcmV0dXJuIG1lbWJlcnM7XG4gICAgfVxuXG4gICAgbGV0IGtpbmQ6IENsYXNzTWVtYmVyS2luZHxudWxsID0gbnVsbDtcbiAgICBpZiAoc3ltYm9sLmZsYWdzICYgdHMuU3ltYm9sRmxhZ3MuTWV0aG9kKSB7XG4gICAgICBraW5kID0gQ2xhc3NNZW1iZXJLaW5kLk1ldGhvZDtcbiAgICB9IGVsc2UgaWYgKHN5bWJvbC5mbGFncyAmIHRzLlN5bWJvbEZsYWdzLlByb3BlcnR5KSB7XG4gICAgICBraW5kID0gQ2xhc3NNZW1iZXJLaW5kLlByb3BlcnR5O1xuICAgIH1cblxuICAgIGNvbnN0IG5vZGUgPSBzeW1ib2wudmFsdWVEZWNsYXJhdGlvbiB8fCBzeW1ib2wuZGVjbGFyYXRpb25zICYmIHN5bWJvbC5kZWNsYXJhdGlvbnNbMF07XG4gICAgaWYgKCFub2RlKSB7XG4gICAgICAvLyBJZiB0aGUgc3ltYm9sIGhhcyBiZWVuIGltcG9ydGVkIGZyb20gYSBUeXBlU2NyaXB0IHR5cGluZ3MgZmlsZSB0aGVuIHRoZSBjb21waWxlclxuICAgICAgLy8gbWF5IHBhc3MgdGhlIGBwcm90b3R5cGVgIHN5bWJvbCBhcyBhbiBleHBvcnQgb2YgdGhlIGNsYXNzLlxuICAgICAgLy8gQnV0IHRoaXMgaGFzIG5vIGRlY2xhcmF0aW9uLiBJbiB0aGlzIGNhc2Ugd2UganVzdCBxdWlldGx5IGlnbm9yZSBpdC5cbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cblxuICAgIGNvbnN0IG1lbWJlciA9IHRoaXMucmVmbGVjdE1lbWJlcihub2RlLCBraW5kLCBkZWNvcmF0b3JzLCBpc1N0YXRpYyk7XG4gICAgaWYgKCFtZW1iZXIpIHtcbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cblxuICAgIHJldHVybiBbbWVtYmVyXTtcbiAgfVxuXG4gIC8qKlxuICAgKiBSZWZsZWN0IG92ZXIgYSBzeW1ib2wgYW5kIGV4dHJhY3QgdGhlIG1lbWJlciBpbmZvcm1hdGlvbiwgY29tYmluaW5nIGl0IHdpdGggdGhlXG4gICAqIHByb3ZpZGVkIGRlY29yYXRvciBpbmZvcm1hdGlvbiwgYW5kIHdoZXRoZXIgaXQgaXMgYSBzdGF0aWMgbWVtYmVyLlxuICAgKiBAcGFyYW0gbm9kZSB0aGUgZGVjbGFyYXRpb24gbm9kZSBmb3IgdGhlIG1lbWJlciB0byByZWZsZWN0IG92ZXIuXG4gICAqIEBwYXJhbSBraW5kIHRoZSBhc3N1bWVkIGtpbmQgb2YgdGhlIG1lbWJlciwgbWF5IGJlY29tZSBtb3JlIGFjY3VyYXRlIGR1cmluZyByZWZsZWN0aW9uLlxuICAgKiBAcGFyYW0gZGVjb3JhdG9ycyBhbiBhcnJheSBvZiBkZWNvcmF0b3JzIGFzc29jaWF0ZWQgd2l0aCB0aGUgbWVtYmVyLlxuICAgKiBAcGFyYW0gaXNTdGF0aWMgdHJ1ZSBpZiB0aGlzIG1lbWJlciBpcyBzdGF0aWMsIGZhbHNlIGlmIGl0IGlzIGFuIGluc3RhbmNlIHByb3BlcnR5LlxuICAgKiBAcmV0dXJucyB0aGUgcmVmbGVjdGVkIG1lbWJlciBpbmZvcm1hdGlvbiwgb3IgbnVsbCBpZiB0aGUgc3ltYm9sIGlzIG5vdCBhIG1lbWJlci5cbiAgICovXG4gIHByb3RlY3RlZCByZWZsZWN0TWVtYmVyKFxuICAgICAgbm9kZTogdHMuRGVjbGFyYXRpb24sIGtpbmQ6IENsYXNzTWVtYmVyS2luZHxudWxsLCBkZWNvcmF0b3JzPzogRGVjb3JhdG9yW10sXG4gICAgICBpc1N0YXRpYz86IGJvb2xlYW4pOiBDbGFzc01lbWJlcnxudWxsIHtcbiAgICBsZXQgdmFsdWU6IHRzLkV4cHJlc3Npb258bnVsbCA9IG51bGw7XG4gICAgbGV0IG5hbWU6IHN0cmluZ3xudWxsID0gbnVsbDtcbiAgICBsZXQgbmFtZU5vZGU6IHRzLklkZW50aWZpZXJ8bnVsbCA9IG51bGw7XG5cbiAgICBpZiAoIWlzQ2xhc3NNZW1iZXJUeXBlKG5vZGUpKSB7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG5cbiAgICBpZiAoaXNTdGF0aWMgJiYgaXNQcm9wZXJ0eUFjY2Vzcyhub2RlKSkge1xuICAgICAgbmFtZSA9IG5vZGUubmFtZS50ZXh0O1xuICAgICAgdmFsdWUgPSBraW5kID09PSBDbGFzc01lbWJlcktpbmQuUHJvcGVydHkgPyBub2RlLnBhcmVudC5yaWdodCA6IG51bGw7XG4gICAgfSBlbHNlIGlmIChpc1RoaXNBc3NpZ25tZW50KG5vZGUpKSB7XG4gICAgICBraW5kID0gQ2xhc3NNZW1iZXJLaW5kLlByb3BlcnR5O1xuICAgICAgbmFtZSA9IG5vZGUubGVmdC5uYW1lLnRleHQ7XG4gICAgICB2YWx1ZSA9IG5vZGUucmlnaHQ7XG4gICAgICBpc1N0YXRpYyA9IGZhbHNlO1xuICAgIH0gZWxzZSBpZiAodHMuaXNDb25zdHJ1Y3RvckRlY2xhcmF0aW9uKG5vZGUpKSB7XG4gICAgICBraW5kID0gQ2xhc3NNZW1iZXJLaW5kLkNvbnN0cnVjdG9yO1xuICAgICAgbmFtZSA9ICdjb25zdHJ1Y3Rvcic7XG4gICAgICBpc1N0YXRpYyA9IGZhbHNlO1xuICAgIH1cblxuICAgIGlmIChraW5kID09PSBudWxsKSB7XG4gICAgICB0aGlzLmxvZ2dlci53YXJuKGBVbmtub3duIG1lbWJlciB0eXBlOiBcIiR7bm9kZS5nZXRUZXh0KCl9YCk7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG5cbiAgICBpZiAoIW5hbWUpIHtcbiAgICAgIGlmIChpc05hbWVkRGVjbGFyYXRpb24obm9kZSkpIHtcbiAgICAgICAgbmFtZSA9IG5vZGUubmFtZS50ZXh0O1xuICAgICAgICBuYW1lTm9kZSA9IG5vZGUubmFtZTtcbiAgICAgIH0gZWxzZSB7XG4gICAgICAgIHJldHVybiBudWxsO1xuICAgICAgfVxuICAgIH1cblxuICAgIC8vIElmIHdlIGhhdmUgc3RpbGwgbm90IGRldGVybWluZWQgaWYgdGhpcyBpcyBhIHN0YXRpYyBvciBpbnN0YW5jZSBtZW1iZXIgdGhlblxuICAgIC8vIGxvb2sgZm9yIHRoZSBgc3RhdGljYCBrZXl3b3JkIG9uIHRoZSBkZWNsYXJhdGlvblxuICAgIGlmIChpc1N0YXRpYyA9PT0gdW5kZWZpbmVkKSB7XG4gICAgICBpc1N0YXRpYyA9IG5vZGUubW9kaWZpZXJzICE9PSB1bmRlZmluZWQgJiZcbiAgICAgICAgICBub2RlLm1vZGlmaWVycy5zb21lKG1vZCA9PiBtb2Qua2luZCA9PT0gdHMuU3ludGF4S2luZC5TdGF0aWNLZXl3b3JkKTtcbiAgICB9XG5cbiAgICBjb25zdCB0eXBlOiB0cy5UeXBlTm9kZSA9IChub2RlIGFzIGFueSkudHlwZSB8fCBudWxsO1xuICAgIHJldHVybiB7XG4gICAgICBub2RlLFxuICAgICAgaW1wbGVtZW50YXRpb246IG5vZGUsIGtpbmQsIHR5cGUsIG5hbWUsIG5hbWVOb2RlLCB2YWx1ZSwgaXNTdGF0aWMsXG4gICAgICBkZWNvcmF0b3JzOiBkZWNvcmF0b3JzIHx8IFtdXG4gICAgfTtcbiAgfVxuXG4gIC8qKlxuICAgKiBGaW5kIHRoZSBkZWNsYXJhdGlvbnMgb2YgdGhlIGNvbnN0cnVjdG9yIHBhcmFtZXRlcnMgb2YgYSBjbGFzcyBpZGVudGlmaWVkIGJ5IGl0cyBzeW1ib2wuXG4gICAqIEBwYXJhbSBjbGFzc1N5bWJvbCB0aGUgY2xhc3Mgd2hvc2UgcGFyYW1ldGVycyB3ZSB3YW50IHRvIGZpbmQuXG4gICAqIEByZXR1cm5zIGFuIGFycmF5IG9mIGB0cy5QYXJhbWV0ZXJEZWNsYXJhdGlvbmAgb2JqZWN0cyByZXByZXNlbnRpbmcgZWFjaCBvZiB0aGUgcGFyYW1ldGVycyBpblxuICAgKiB0aGUgY2xhc3MncyBjb25zdHJ1Y3RvciBvciBudWxsIGlmIHRoZXJlIGlzIG5vIGNvbnN0cnVjdG9yLlxuICAgKi9cbiAgcHJvdGVjdGVkIGdldENvbnN0cnVjdG9yUGFyYW1ldGVyRGVjbGFyYXRpb25zKGNsYXNzU3ltYm9sOiBDbGFzc1N5bWJvbCk6XG4gICAgICB0cy5QYXJhbWV0ZXJEZWNsYXJhdGlvbltdfG51bGwge1xuICAgIGlmIChjbGFzc1N5bWJvbC5tZW1iZXJzICYmIGNsYXNzU3ltYm9sLm1lbWJlcnMuaGFzKENPTlNUUlVDVE9SKSkge1xuICAgICAgY29uc3QgY29uc3RydWN0b3JTeW1ib2wgPSBjbGFzc1N5bWJvbC5tZW1iZXJzLmdldChDT05TVFJVQ1RPUikgITtcbiAgICAgIC8vIEZvciBzb21lIHJlYXNvbiB0aGUgY29uc3RydWN0b3IgZG9lcyBub3QgaGF2ZSBhIGB2YWx1ZURlY2xhcmF0aW9uYCA/IT9cbiAgICAgIGNvbnN0IGNvbnN0cnVjdG9yID0gY29uc3RydWN0b3JTeW1ib2wuZGVjbGFyYXRpb25zICYmXG4gICAgICAgICAgY29uc3RydWN0b3JTeW1ib2wuZGVjbGFyYXRpb25zWzBdIGFzIHRzLkNvbnN0cnVjdG9yRGVjbGFyYXRpb24gfCB1bmRlZmluZWQ7XG4gICAgICBpZiAoIWNvbnN0cnVjdG9yKSB7XG4gICAgICAgIHJldHVybiBbXTtcbiAgICAgIH1cbiAgICAgIGlmIChjb25zdHJ1Y3Rvci5wYXJhbWV0ZXJzLmxlbmd0aCA+IDApIHtcbiAgICAgICAgcmV0dXJuIEFycmF5LmZyb20oY29uc3RydWN0b3IucGFyYW1ldGVycyk7XG4gICAgICB9XG4gICAgICBpZiAoaXNTeW50aGVzaXplZENvbnN0cnVjdG9yKGNvbnN0cnVjdG9yKSkge1xuICAgICAgICByZXR1cm4gbnVsbDtcbiAgICAgIH1cbiAgICAgIHJldHVybiBbXTtcbiAgICB9XG4gICAgcmV0dXJuIG51bGw7XG4gIH1cblxuICAvKipcbiAgICogR2V0IHRoZSBwYXJhbWV0ZXIgZGVjb3JhdG9ycyBvZiBhIGNsYXNzIGNvbnN0cnVjdG9yLlxuICAgKlxuICAgKiBAcGFyYW0gY2xhc3NTeW1ib2wgdGhlIGNsYXNzIHdob3NlIHBhcmFtZXRlciBpbmZvIHdlIHdhbnQgdG8gZ2V0LlxuICAgKiBAcGFyYW0gcGFyYW1ldGVyTm9kZXMgdGhlIGFycmF5IG9mIFR5cGVTY3JpcHQgcGFyYW1ldGVyIG5vZGVzIGZvciB0aGlzIGNsYXNzJ3MgY29uc3RydWN0b3IuXG4gICAqIEByZXR1cm5zIGFuIGFycmF5IG9mIGNvbnN0cnVjdG9yIHBhcmFtZXRlciBpbmZvIG9iamVjdHMuXG4gICAqL1xuICBwcm90ZWN0ZWQgZ2V0Q29uc3RydWN0b3JQYXJhbUluZm8oXG4gICAgICBjbGFzc1N5bWJvbDogQ2xhc3NTeW1ib2wsIHBhcmFtZXRlck5vZGVzOiB0cy5QYXJhbWV0ZXJEZWNsYXJhdGlvbltdKTogQ3RvclBhcmFtZXRlcltdIHtcbiAgICBjb25zdCBwYXJhbXNQcm9wZXJ0eSA9IHRoaXMuZ2V0U3RhdGljUHJvcGVydHkoY2xhc3NTeW1ib2wsIENPTlNUUlVDVE9SX1BBUkFNUyk7XG4gICAgY29uc3QgcGFyYW1JbmZvOiBQYXJhbUluZm9bXXxudWxsID0gcGFyYW1zUHJvcGVydHkgP1xuICAgICAgICB0aGlzLmdldFBhcmFtSW5mb0Zyb21TdGF0aWNQcm9wZXJ0eShwYXJhbXNQcm9wZXJ0eSkgOlxuICAgICAgICB0aGlzLmdldFBhcmFtSW5mb0Zyb21IZWxwZXJDYWxsKGNsYXNzU3ltYm9sLCBwYXJhbWV0ZXJOb2Rlcyk7XG5cbiAgICByZXR1cm4gcGFyYW1ldGVyTm9kZXMubWFwKChub2RlLCBpbmRleCkgPT4ge1xuICAgICAgY29uc3Qge2RlY29yYXRvcnMsIHR5cGVFeHByZXNzaW9ufSA9IHBhcmFtSW5mbyAmJiBwYXJhbUluZm9baW5kZXhdID9cbiAgICAgICAgICBwYXJhbUluZm9baW5kZXhdIDpcbiAgICAgICAgICB7ZGVjb3JhdG9yczogbnVsbCwgdHlwZUV4cHJlc3Npb246IG51bGx9O1xuICAgICAgY29uc3QgbmFtZU5vZGUgPSBub2RlLm5hbWU7XG4gICAgICByZXR1cm4ge1xuICAgICAgICBuYW1lOiBnZXROYW1lVGV4dChuYW1lTm9kZSksXG4gICAgICAgIG5hbWVOb2RlLFxuICAgICAgICB0eXBlVmFsdWVSZWZlcmVuY2U6IHR5cGVFeHByZXNzaW9uICE9PSBudWxsID9cbiAgICAgICAgICAgIHtsb2NhbDogdHJ1ZSBhcyB0cnVlLCBleHByZXNzaW9uOiB0eXBlRXhwcmVzc2lvbiwgZGVmYXVsdEltcG9ydFN0YXRlbWVudDogbnVsbH0gOlxuICAgICAgICAgICAgbnVsbCxcbiAgICAgICAgdHlwZU5vZGU6IG51bGwsIGRlY29yYXRvcnNcbiAgICAgIH07XG4gICAgfSk7XG4gIH1cblxuICAvKipcbiAgICogR2V0IHRoZSBwYXJhbWV0ZXIgdHlwZSBhbmQgZGVjb3JhdG9ycyBmb3IgdGhlIGNvbnN0cnVjdG9yIG9mIGEgY2xhc3MsXG4gICAqIHdoZXJlIHRoZSBpbmZvcm1hdGlvbiBpcyBzdG9yZWQgb24gYSBzdGF0aWMgcHJvcGVydHkgb2YgdGhlIGNsYXNzLlxuICAgKlxuICAgKiBOb3RlIHRoYXQgaW4gRVNNMjAxNSwgdGhlIHByb3BlcnR5IGlzIGRlZmluZWQgYW4gYXJyYXksIG9yIGJ5IGFuIGFycm93IGZ1bmN0aW9uIHRoYXQgcmV0dXJucyBhblxuICAgKiBhcnJheSwgb2YgZGVjb3JhdG9yIGFuZCB0eXBlIGluZm9ybWF0aW9uLlxuICAgKlxuICAgKiBGb3IgZXhhbXBsZSxcbiAgICpcbiAgICogYGBgXG4gICAqIFNvbWVEaXJlY3RpdmUuY3RvclBhcmFtZXRlcnMgPSAoKSA9PiBbXG4gICAqICAge3R5cGU6IFZpZXdDb250YWluZXJSZWZ9LFxuICAgKiAgIHt0eXBlOiBUZW1wbGF0ZVJlZn0sXG4gICAqICAge3R5cGU6IHVuZGVmaW5lZCwgZGVjb3JhdG9yczogW3sgdHlwZTogSW5qZWN0LCBhcmdzOiBbSU5KRUNURURfVE9LRU5dfV19LFxuICAgKiBdO1xuICAgKiBgYGBcbiAgICpcbiAgICogb3JcbiAgICpcbiAgICogYGBgXG4gICAqIFNvbWVEaXJlY3RpdmUuY3RvclBhcmFtZXRlcnMgPSBbXG4gICAqICAge3R5cGU6IFZpZXdDb250YWluZXJSZWZ9LFxuICAgKiAgIHt0eXBlOiBUZW1wbGF0ZVJlZn0sXG4gICAqICAge3R5cGU6IHVuZGVmaW5lZCwgZGVjb3JhdG9yczogW3t0eXBlOiBJbmplY3QsIGFyZ3M6IFtJTkpFQ1RFRF9UT0tFTl19XX0sXG4gICAqIF07XG4gICAqIGBgYFxuICAgKlxuICAgKiBAcGFyYW0gcGFyYW1EZWNvcmF0b3JzUHJvcGVydHkgdGhlIHByb3BlcnR5IHRoYXQgaG9sZHMgdGhlIHBhcmFtZXRlciBpbmZvIHdlIHdhbnQgdG8gZ2V0LlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBvYmplY3RzIGNvbnRhaW5pbmcgdGhlIHR5cGUgYW5kIGRlY29yYXRvcnMgZm9yIGVhY2ggcGFyYW1ldGVyLlxuICAgKi9cbiAgcHJvdGVjdGVkIGdldFBhcmFtSW5mb0Zyb21TdGF0aWNQcm9wZXJ0eShwYXJhbURlY29yYXRvcnNQcm9wZXJ0eTogdHMuU3ltYm9sKTogUGFyYW1JbmZvW118bnVsbCB7XG4gICAgY29uc3QgcGFyYW1EZWNvcmF0b3JzID0gZ2V0UHJvcGVydHlWYWx1ZUZyb21TeW1ib2wocGFyYW1EZWNvcmF0b3JzUHJvcGVydHkpO1xuICAgIGlmIChwYXJhbURlY29yYXRvcnMpIHtcbiAgICAgIC8vIFRoZSBkZWNvcmF0b3JzIGFycmF5IG1heSBiZSB3cmFwcGVkIGluIGFuIGFycm93IGZ1bmN0aW9uLiBJZiBzbyB1bndyYXAgaXQuXG4gICAgICBjb25zdCBjb250YWluZXIgPVxuICAgICAgICAgIHRzLmlzQXJyb3dGdW5jdGlvbihwYXJhbURlY29yYXRvcnMpID8gcGFyYW1EZWNvcmF0b3JzLmJvZHkgOiBwYXJhbURlY29yYXRvcnM7XG4gICAgICBpZiAodHMuaXNBcnJheUxpdGVyYWxFeHByZXNzaW9uKGNvbnRhaW5lcikpIHtcbiAgICAgICAgY29uc3QgZWxlbWVudHMgPSBjb250YWluZXIuZWxlbWVudHM7XG4gICAgICAgIHJldHVybiBlbGVtZW50c1xuICAgICAgICAgICAgLm1hcChcbiAgICAgICAgICAgICAgICBlbGVtZW50ID0+XG4gICAgICAgICAgICAgICAgICAgIHRzLmlzT2JqZWN0TGl0ZXJhbEV4cHJlc3Npb24oZWxlbWVudCkgPyByZWZsZWN0T2JqZWN0TGl0ZXJhbChlbGVtZW50KSA6IG51bGwpXG4gICAgICAgICAgICAubWFwKHBhcmFtSW5mbyA9PiB7XG4gICAgICAgICAgICAgIGNvbnN0IHR5cGVFeHByZXNzaW9uID1cbiAgICAgICAgICAgICAgICAgIHBhcmFtSW5mbyAmJiBwYXJhbUluZm8uaGFzKCd0eXBlJykgPyBwYXJhbUluZm8uZ2V0KCd0eXBlJykgISA6IG51bGw7XG4gICAgICAgICAgICAgIGNvbnN0IGRlY29yYXRvckluZm8gPVxuICAgICAgICAgICAgICAgICAgcGFyYW1JbmZvICYmIHBhcmFtSW5mby5oYXMoJ2RlY29yYXRvcnMnKSA/IHBhcmFtSW5mby5nZXQoJ2RlY29yYXRvcnMnKSAhIDogbnVsbDtcbiAgICAgICAgICAgICAgY29uc3QgZGVjb3JhdG9ycyA9IGRlY29yYXRvckluZm8gJiZcbiAgICAgICAgICAgICAgICAgIHRoaXMucmVmbGVjdERlY29yYXRvcnMoZGVjb3JhdG9ySW5mbylcbiAgICAgICAgICAgICAgICAgICAgICAuZmlsdGVyKGRlY29yYXRvciA9PiB0aGlzLmlzRnJvbUNvcmUoZGVjb3JhdG9yKSk7XG4gICAgICAgICAgICAgIHJldHVybiB7dHlwZUV4cHJlc3Npb24sIGRlY29yYXRvcnN9O1xuICAgICAgICAgICAgfSk7XG4gICAgICB9IGVsc2UgaWYgKHBhcmFtRGVjb3JhdG9ycyAhPT0gdW5kZWZpbmVkKSB7XG4gICAgICAgIHRoaXMubG9nZ2VyLndhcm4oXG4gICAgICAgICAgICAnSW52YWxpZCBjb25zdHJ1Y3RvciBwYXJhbWV0ZXIgZGVjb3JhdG9yIGluICcgK1xuICAgICAgICAgICAgICAgIHBhcmFtRGVjb3JhdG9ycy5nZXRTb3VyY2VGaWxlKCkuZmlsZU5hbWUgKyAnOlxcbicsXG4gICAgICAgICAgICBwYXJhbURlY29yYXRvcnMuZ2V0VGV4dCgpKTtcbiAgICAgIH1cbiAgICB9XG4gICAgcmV0dXJuIG51bGw7XG4gIH1cblxuICAvKipcbiAgICogR2V0IHRoZSBwYXJhbWV0ZXIgdHlwZSBhbmQgZGVjb3JhdG9ycyBmb3IgYSBjbGFzcyB3aGVyZSB0aGUgaW5mb3JtYXRpb24gaXMgc3RvcmVkIHZpYVxuICAgKiBjYWxscyB0byBgX19kZWNvcmF0ZWAgaGVscGVycy5cbiAgICpcbiAgICogUmVmbGVjdCBvdmVyIHRoZSBoZWxwZXJzIHRvIGZpbmQgdGhlIGRlY29yYXRvcnMgYW5kIHR5cGVzIGFib3V0IGVhY2ggb2ZcbiAgICogdGhlIGNsYXNzJ3MgY29uc3RydWN0b3IgcGFyYW1ldGVycy5cbiAgICpcbiAgICogQHBhcmFtIGNsYXNzU3ltYm9sIHRoZSBjbGFzcyB3aG9zZSBwYXJhbWV0ZXIgaW5mbyB3ZSB3YW50IHRvIGdldC5cbiAgICogQHBhcmFtIHBhcmFtZXRlck5vZGVzIHRoZSBhcnJheSBvZiBUeXBlU2NyaXB0IHBhcmFtZXRlciBub2RlcyBmb3IgdGhpcyBjbGFzcydzIGNvbnN0cnVjdG9yLlxuICAgKiBAcmV0dXJucyBhbiBhcnJheSBvZiBvYmplY3RzIGNvbnRhaW5pbmcgdGhlIHR5cGUgYW5kIGRlY29yYXRvcnMgZm9yIGVhY2ggcGFyYW1ldGVyLlxuICAgKi9cbiAgcHJvdGVjdGVkIGdldFBhcmFtSW5mb0Zyb21IZWxwZXJDYWxsKFxuICAgICAgY2xhc3NTeW1ib2w6IENsYXNzU3ltYm9sLCBwYXJhbWV0ZXJOb2RlczogdHMuUGFyYW1ldGVyRGVjbGFyYXRpb25bXSk6IFBhcmFtSW5mb1tdIHtcbiAgICBjb25zdCBwYXJhbWV0ZXJzOiBQYXJhbUluZm9bXSA9XG4gICAgICAgIHBhcmFtZXRlck5vZGVzLm1hcCgoKSA9PiAoe3R5cGVFeHByZXNzaW9uOiBudWxsLCBkZWNvcmF0b3JzOiBudWxsfSkpO1xuICAgIGNvbnN0IGhlbHBlckNhbGxzID0gdGhpcy5nZXRIZWxwZXJDYWxsc0ZvckNsYXNzKGNsYXNzU3ltYm9sLCAnX19kZWNvcmF0ZScpO1xuICAgIGhlbHBlckNhbGxzLmZvckVhY2goaGVscGVyQ2FsbCA9PiB7XG4gICAgICBjb25zdCB7Y2xhc3NEZWNvcmF0b3JzfSA9XG4gICAgICAgICAgdGhpcy5yZWZsZWN0RGVjb3JhdG9yc0Zyb21IZWxwZXJDYWxsKGhlbHBlckNhbGwsIG1ha2VDbGFzc1RhcmdldEZpbHRlcihjbGFzc1N5bWJvbC5uYW1lKSk7XG4gICAgICBjbGFzc0RlY29yYXRvcnMuZm9yRWFjaChjYWxsID0+IHtcbiAgICAgICAgc3dpdGNoIChjYWxsLm5hbWUpIHtcbiAgICAgICAgICBjYXNlICdfX21ldGFkYXRhJzpcbiAgICAgICAgICAgIGNvbnN0IG1ldGFkYXRhQXJnID0gY2FsbC5hcmdzICYmIGNhbGwuYXJnc1swXTtcbiAgICAgICAgICAgIGNvbnN0IHR5cGVzQXJnID0gY2FsbC5hcmdzICYmIGNhbGwuYXJnc1sxXTtcbiAgICAgICAgICAgIGNvbnN0IGlzUGFyYW1UeXBlRGVjb3JhdG9yID0gbWV0YWRhdGFBcmcgJiYgdHMuaXNTdHJpbmdMaXRlcmFsKG1ldGFkYXRhQXJnKSAmJlxuICAgICAgICAgICAgICAgIG1ldGFkYXRhQXJnLnRleHQgPT09ICdkZXNpZ246cGFyYW10eXBlcyc7XG4gICAgICAgICAgICBjb25zdCB0eXBlcyA9IHR5cGVzQXJnICYmIHRzLmlzQXJyYXlMaXRlcmFsRXhwcmVzc2lvbih0eXBlc0FyZykgJiYgdHlwZXNBcmcuZWxlbWVudHM7XG4gICAgICAgICAgICBpZiAoaXNQYXJhbVR5cGVEZWNvcmF0b3IgJiYgdHlwZXMpIHtcbiAgICAgICAgICAgICAgdHlwZXMuZm9yRWFjaCgodHlwZSwgaW5kZXgpID0+IHBhcmFtZXRlcnNbaW5kZXhdLnR5cGVFeHByZXNzaW9uID0gdHlwZSk7XG4gICAgICAgICAgICB9XG4gICAgICAgICAgICBicmVhaztcbiAgICAgICAgICBjYXNlICdfX3BhcmFtJzpcbiAgICAgICAgICAgIGNvbnN0IHBhcmFtSW5kZXhBcmcgPSBjYWxsLmFyZ3MgJiYgY2FsbC5hcmdzWzBdO1xuICAgICAgICAgICAgY29uc3QgZGVjb3JhdG9yQ2FsbEFyZyA9IGNhbGwuYXJncyAmJiBjYWxsLmFyZ3NbMV07XG4gICAgICAgICAgICBjb25zdCBwYXJhbUluZGV4ID0gcGFyYW1JbmRleEFyZyAmJiB0cy5pc051bWVyaWNMaXRlcmFsKHBhcmFtSW5kZXhBcmcpID9cbiAgICAgICAgICAgICAgICBwYXJzZUludChwYXJhbUluZGV4QXJnLnRleHQsIDEwKSA6XG4gICAgICAgICAgICAgICAgTmFOO1xuICAgICAgICAgICAgY29uc3QgZGVjb3JhdG9yID0gZGVjb3JhdG9yQ2FsbEFyZyAmJiB0cy5pc0NhbGxFeHByZXNzaW9uKGRlY29yYXRvckNhbGxBcmcpID9cbiAgICAgICAgICAgICAgICB0aGlzLnJlZmxlY3REZWNvcmF0b3JDYWxsKGRlY29yYXRvckNhbGxBcmcpIDpcbiAgICAgICAgICAgICAgICBudWxsO1xuICAgICAgICAgICAgaWYgKCFpc05hTihwYXJhbUluZGV4KSAmJiBkZWNvcmF0b3IpIHtcbiAgICAgICAgICAgICAgY29uc3QgZGVjb3JhdG9ycyA9IHBhcmFtZXRlcnNbcGFyYW1JbmRleF0uZGVjb3JhdG9ycyA9XG4gICAgICAgICAgICAgICAgICBwYXJhbWV0ZXJzW3BhcmFtSW5kZXhdLmRlY29yYXRvcnMgfHwgW107XG4gICAgICAgICAgICAgIGRlY29yYXRvcnMucHVzaChkZWNvcmF0b3IpO1xuICAgICAgICAgICAgfVxuICAgICAgICAgICAgYnJlYWs7XG4gICAgICAgIH1cbiAgICAgIH0pO1xuICAgIH0pO1xuICAgIHJldHVybiBwYXJhbWV0ZXJzO1xuICB9XG5cbiAgLyoqXG4gICAqIFNlYXJjaCBzdGF0ZW1lbnRzIHJlbGF0ZWQgdG8gdGhlIGdpdmVuIGNsYXNzIGZvciBjYWxscyB0byB0aGUgc3BlY2lmaWVkIGhlbHBlci5cbiAgICogQHBhcmFtIGNsYXNzU3ltYm9sIHRoZSBjbGFzcyB3aG9zZSBoZWxwZXIgY2FsbHMgd2UgYXJlIGludGVyZXN0ZWQgaW4uXG4gICAqIEBwYXJhbSBoZWxwZXJOYW1lIHRoZSBuYW1lIG9mIHRoZSBoZWxwZXIgKGUuZy4gYF9fZGVjb3JhdGVgKSB3aG9zZSBjYWxscyB3ZSBhcmUgaW50ZXJlc3RlZFxuICAgKiBpbi5cbiAgICogQHJldHVybnMgYW4gYXJyYXkgb2YgQ2FsbEV4cHJlc3Npb24gbm9kZXMgZm9yIGVhY2ggbWF0Y2hpbmcgaGVscGVyIGNhbGwuXG4gICAqL1xuICBwcm90ZWN0ZWQgZ2V0SGVscGVyQ2FsbHNGb3JDbGFzcyhjbGFzc1N5bWJvbDogQ2xhc3NTeW1ib2wsIGhlbHBlck5hbWU6IHN0cmluZyk6XG4gICAgICB0cy5DYWxsRXhwcmVzc2lvbltdIHtcbiAgICByZXR1cm4gdGhpcy5nZXRTdGF0ZW1lbnRzRm9yQ2xhc3MoY2xhc3NTeW1ib2wpXG4gICAgICAgIC5tYXAoc3RhdGVtZW50ID0+IHRoaXMuZ2V0SGVscGVyQ2FsbChzdGF0ZW1lbnQsIGhlbHBlck5hbWUpKVxuICAgICAgICAuZmlsdGVyKGlzRGVmaW5lZCk7XG4gIH1cblxuICAvKipcbiAgICogRmluZCBzdGF0ZW1lbnRzIHJlbGF0ZWQgdG8gdGhlIGdpdmVuIGNsYXNzIHRoYXQgbWF5IGNvbnRhaW4gY2FsbHMgdG8gYSBoZWxwZXIuXG4gICAqXG4gICAqIEluIEVTTTIwMTUgY29kZSB0aGUgaGVscGVyIGNhbGxzIGFyZSBpbiB0aGUgdG9wIGxldmVsIG1vZHVsZSwgc28gd2UgaGF2ZSB0byBjb25zaWRlclxuICAgKiBhbGwgdGhlIHN0YXRlbWVudHMgaW4gdGhlIG1vZHVsZS5cbiAgICpcbiAgICogQHBhcmFtIGNsYXNzU3ltYm9sIHRoZSBjbGFzcyB3aG9zZSBoZWxwZXIgY2FsbHMgd2UgYXJlIGludGVyZXN0ZWQgaW4uXG4gICAqIEByZXR1cm5zIGFuIGFycmF5IG9mIHN0YXRlbWVudHMgdGhhdCBtYXkgY29udGFpbiBoZWxwZXIgY2FsbHMuXG4gICAqL1xuICBwcm90ZWN0ZWQgZ2V0U3RhdGVtZW50c0ZvckNsYXNzKGNsYXNzU3ltYm9sOiBDbGFzc1N5bWJvbCk6IHRzLlN0YXRlbWVudFtdIHtcbiAgICByZXR1cm4gQXJyYXkuZnJvbShjbGFzc1N5bWJvbC52YWx1ZURlY2xhcmF0aW9uLmdldFNvdXJjZUZpbGUoKS5zdGF0ZW1lbnRzKTtcbiAgfVxuXG4gIC8qKlxuICAgKiBUZXN0IHdoZXRoZXIgYSBkZWNvcmF0b3Igd2FzIGltcG9ydGVkIGZyb20gYEBhbmd1bGFyL2NvcmVgLlxuICAgKlxuICAgKiBJcyB0aGUgZGVjb3JhdG9yOlxuICAgKiAqIGV4dGVybmFsbHkgaW1wb3J0ZWQgZnJvbSBgQGFuZ3VsYXIvY29yZWA/XG4gICAqICogdGhlIGN1cnJlbnQgaG9zdGVkIHByb2dyYW0gaXMgYWN0dWFsbHkgYEBhbmd1bGFyL2NvcmVgIGFuZFxuICAgKiAgIC0gcmVsYXRpdmVseSBpbnRlcm5hbGx5IGltcG9ydGVkOyBvclxuICAgKiAgIC0gbm90IGltcG9ydGVkLCBmcm9tIHRoZSBjdXJyZW50IGZpbGUuXG4gICAqXG4gICAqIEBwYXJhbSBkZWNvcmF0b3IgdGhlIGRlY29yYXRvciB0byB0ZXN0LlxuICAgKi9cbiAgcHJvdGVjdGVkIGlzRnJvbUNvcmUoZGVjb3JhdG9yOiBEZWNvcmF0b3IpOiBib29sZWFuIHtcbiAgICBpZiAodGhpcy5pc0NvcmUpIHtcbiAgICAgIHJldHVybiAhZGVjb3JhdG9yLmltcG9ydCB8fCAvXlxcLi8udGVzdChkZWNvcmF0b3IuaW1wb3J0LmZyb20pO1xuICAgIH0gZWxzZSB7XG4gICAgICByZXR1cm4gISFkZWNvcmF0b3IuaW1wb3J0ICYmIGRlY29yYXRvci5pbXBvcnQuZnJvbSA9PT0gJ0Bhbmd1bGFyL2NvcmUnO1xuICAgIH1cbiAgfVxuXG4gIC8qKlxuICAgKiBFeHRyYWN0IGFsbCB0aGUgY2xhc3MgZGVjbGFyYXRpb25zIGZyb20gdGhlIGR0c1R5cGluZ3MgcHJvZ3JhbSwgc3RvcmluZyB0aGVtIGluIGEgbWFwXG4gICAqIHdoZXJlIHRoZSBrZXkgaXMgdGhlIGRlY2xhcmVkIG5hbWUgb2YgdGhlIGNsYXNzIGFuZCB0aGUgdmFsdWUgaXMgdGhlIGRlY2xhcmF0aW9uIGl0c2VsZi5cbiAgICpcbiAgICogSXQgaXMgcG9zc2libGUgZm9yIHRoZXJlIHRvIGJlIG11bHRpcGxlIGNsYXNzIGRlY2xhcmF0aW9ucyB3aXRoIHRoZSBzYW1lIGxvY2FsIG5hbWUuXG4gICAqIE9ubHkgdGhlIGZpcnN0IGRlY2xhcmF0aW9uIHdpdGggYSBnaXZlbiBuYW1lIGlzIGFkZGVkIHRvIHRoZSBtYXA7IHN1YnNlcXVlbnQgY2xhc3NlcyB3aWxsIGJlXG4gICAqIGlnbm9yZWQuXG4gICAqXG4gICAqIFdlIGFyZSBtb3N0IGludGVyZXN0ZWQgaW4gY2xhc3NlcyB0aGF0IGFyZSBwdWJsaWNseSBleHBvcnRlZCBmcm9tIHRoZSBlbnRyeSBwb2ludCwgc28gdGhlc2VcbiAgICogYXJlIGFkZGVkIHRvIHRoZSBtYXAgZmlyc3QsIHRvIGVuc3VyZSB0aGF0IHRoZXkgYXJlIG5vdCBpZ25vcmVkLlxuICAgKlxuICAgKiBAcGFyYW0gZHRzUm9vdEZpbGVOYW1lIFRoZSBmaWxlbmFtZSBvZiB0aGUgZW50cnktcG9pbnQgdG8gdGhlIGBkdHNUeXBpbmdzYCBwcm9ncmFtLlxuICAgKiBAcGFyYW0gZHRzUHJvZ3JhbSBUaGUgcHJvZ3JhbSBjb250YWluaW5nIGFsbCB0aGUgdHlwaW5ncyBmaWxlcy5cbiAgICogQHJldHVybnMgYSBtYXAgb2YgY2xhc3MgbmFtZXMgdG8gY2xhc3MgZGVjbGFyYXRpb25zLlxuICAgKi9cbiAgcHJvdGVjdGVkIGNvbXB1dGVEdHNEZWNsYXJhdGlvbk1hcChkdHNSb290RmlsZU5hbWU6IEFic29sdXRlRnNQYXRoLCBkdHNQcm9ncmFtOiB0cy5Qcm9ncmFtKTpcbiAgICAgIE1hcDxzdHJpbmcsIHRzLkRlY2xhcmF0aW9uPiB7XG4gICAgY29uc3QgZHRzRGVjbGFyYXRpb25NYXAgPSBuZXcgTWFwPHN0cmluZywgdHMuRGVjbGFyYXRpb24+KCk7XG4gICAgY29uc3QgY2hlY2tlciA9IGR0c1Byb2dyYW0uZ2V0VHlwZUNoZWNrZXIoKTtcblxuICAgIC8vIEZpcnN0IGFkZCBhbGwgdGhlIGNsYXNzZXMgdGhhdCBhcmUgcHVibGljbHkgZXhwb3J0ZWQgZnJvbSB0aGUgZW50cnktcG9pbnRcbiAgICBjb25zdCByb290RmlsZSA9IGR0c1Byb2dyYW0uZ2V0U291cmNlRmlsZShkdHNSb290RmlsZU5hbWUpO1xuICAgIGlmICghcm9vdEZpbGUpIHtcbiAgICAgIHRocm93IG5ldyBFcnJvcihgVGhlIGdpdmVuIGZpbGUgJHtkdHNSb290RmlsZU5hbWV9IGlzIG5vdCBwYXJ0IG9mIHRoZSB0eXBpbmdzIHByb2dyYW0uYCk7XG4gICAgfVxuICAgIGNvbGxlY3RFeHBvcnRlZERlY2xhcmF0aW9ucyhjaGVja2VyLCBkdHNEZWNsYXJhdGlvbk1hcCwgcm9vdEZpbGUpO1xuXG4gICAgLy8gTm93IGFkZCBhbnkgYWRkaXRpb25hbCBjbGFzc2VzIHRoYXQgYXJlIGV4cG9ydGVkIGZyb20gaW5kaXZpZHVhbCAgZHRzIGZpbGVzLFxuICAgIC8vIGJ1dCBhcmUgbm90IHB1YmxpY2x5IGV4cG9ydGVkIGZyb20gdGhlIGVudHJ5LXBvaW50LlxuICAgIGR0c1Byb2dyYW0uZ2V0U291cmNlRmlsZXMoKS5mb3JFYWNoKFxuICAgICAgICBzb3VyY2VGaWxlID0+IHsgY29sbGVjdEV4cG9ydGVkRGVjbGFyYXRpb25zKGNoZWNrZXIsIGR0c0RlY2xhcmF0aW9uTWFwLCBzb3VyY2VGaWxlKTsgfSk7XG4gICAgcmV0dXJuIGR0c0RlY2xhcmF0aW9uTWFwO1xuICB9XG5cbiAgLyoqXG4gICAqIFBhcnNlIGEgZnVuY3Rpb24vbWV0aG9kIG5vZGUgKG9yIGl0cyBpbXBsZW1lbnRhdGlvbiksIHRvIHNlZSBpZiBpdCByZXR1cm5zIGFcbiAgICogYE1vZHVsZVdpdGhQcm92aWRlcnNgIG9iamVjdC5cbiAgICogQHBhcmFtIG5hbWUgVGhlIG5hbWUgb2YgdGhlIGZ1bmN0aW9uLlxuICAgKiBAcGFyYW0gbm9kZSB0aGUgbm9kZSB0byBjaGVjayAtIHRoaXMgY291bGQgYmUgYSBmdW5jdGlvbiwgYSBtZXRob2Qgb3IgYSB2YXJpYWJsZSBkZWNsYXJhdGlvbi5cbiAgICogQHBhcmFtIGltcGxlbWVudGF0aW9uIHRoZSBhY3R1YWwgZnVuY3Rpb24gZXhwcmVzc2lvbiBpZiBgbm9kZWAgaXMgYSB2YXJpYWJsZSBkZWNsYXJhdGlvbi5cbiAgICogQHBhcmFtIGNvbnRhaW5lciB0aGUgY2xhc3MgdGhhdCBjb250YWlucyB0aGUgZnVuY3Rpb24sIGlmIGl0IGlzIGEgbWV0aG9kLlxuICAgKiBAcmV0dXJucyBpbmZvIGFib3V0IHRoZSBmdW5jdGlvbiBpZiBpdCBkb2VzIHJldHVybiBhIGBNb2R1bGVXaXRoUHJvdmlkZXJzYCBvYmplY3Q7IGBudWxsYFxuICAgKiBvdGhlcndpc2UuXG4gICAqL1xuICBwcm90ZWN0ZWQgcGFyc2VGb3JNb2R1bGVXaXRoUHJvdmlkZXJzKFxuICAgICAgbmFtZTogc3RyaW5nLCBub2RlOiB0cy5Ob2RlfG51bGwsIGltcGxlbWVudGF0aW9uOiB0cy5Ob2RlfG51bGwgPSBub2RlLFxuICAgICAgY29udGFpbmVyOiB0cy5EZWNsYXJhdGlvbnxudWxsID0gbnVsbCk6IE1vZHVsZVdpdGhQcm92aWRlcnNGdW5jdGlvbnxudWxsIHtcbiAgICBpZiAoaW1wbGVtZW50YXRpb24gPT09IG51bGwgfHxcbiAgICAgICAgKCF0cy5pc0Z1bmN0aW9uRGVjbGFyYXRpb24oaW1wbGVtZW50YXRpb24pICYmICF0cy5pc01ldGhvZERlY2xhcmF0aW9uKGltcGxlbWVudGF0aW9uKSAmJlxuICAgICAgICAgIXRzLmlzRnVuY3Rpb25FeHByZXNzaW9uKGltcGxlbWVudGF0aW9uKSkpIHtcbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cbiAgICBjb25zdCBkZWNsYXJhdGlvbiA9IGltcGxlbWVudGF0aW9uO1xuICAgIGNvbnN0IGRlZmluaXRpb24gPSB0aGlzLmdldERlZmluaXRpb25PZkZ1bmN0aW9uKGRlY2xhcmF0aW9uKTtcbiAgICBpZiAoZGVmaW5pdGlvbiA9PT0gbnVsbCkge1xuICAgICAgcmV0dXJuIG51bGw7XG4gICAgfVxuICAgIGNvbnN0IGJvZHkgPSBkZWZpbml0aW9uLmJvZHk7XG4gICAgY29uc3QgbGFzdFN0YXRlbWVudCA9IGJvZHkgJiYgYm9keVtib2R5Lmxlbmd0aCAtIDFdO1xuICAgIGNvbnN0IHJldHVybkV4cHJlc3Npb24gPVxuICAgICAgICBsYXN0U3RhdGVtZW50ICYmIHRzLmlzUmV0dXJuU3RhdGVtZW50KGxhc3RTdGF0ZW1lbnQpICYmIGxhc3RTdGF0ZW1lbnQuZXhwcmVzc2lvbiB8fCBudWxsO1xuICAgIGNvbnN0IG5nTW9kdWxlUHJvcGVydHkgPSByZXR1cm5FeHByZXNzaW9uICYmIHRzLmlzT2JqZWN0TGl0ZXJhbEV4cHJlc3Npb24ocmV0dXJuRXhwcmVzc2lvbikgJiZcbiAgICAgICAgICAgIHJldHVybkV4cHJlc3Npb24ucHJvcGVydGllcy5maW5kKFxuICAgICAgICAgICAgICAgIHByb3AgPT5cbiAgICAgICAgICAgICAgICAgICAgISFwcm9wLm5hbWUgJiYgdHMuaXNJZGVudGlmaWVyKHByb3AubmFtZSkgJiYgcHJvcC5uYW1lLnRleHQgPT09ICduZ01vZHVsZScpIHx8XG4gICAgICAgIG51bGw7XG5cbiAgICBpZiAoIW5nTW9kdWxlUHJvcGVydHkgfHwgIXRzLmlzUHJvcGVydHlBc3NpZ25tZW50KG5nTW9kdWxlUHJvcGVydHkpKSB7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG5cbiAgICAvLyBUaGUgbmdNb2R1bGVWYWx1ZSBjb3VsZCBiZSBvZiB0aGUgZm9ybSBgU29tZU1vZHVsZWAgb3IgYG5hbWVzcGFjZV8xLlNvbWVNb2R1bGVgXG4gICAgY29uc3QgbmdNb2R1bGVWYWx1ZSA9IG5nTW9kdWxlUHJvcGVydHkuaW5pdGlhbGl6ZXI7XG4gICAgaWYgKCF0cy5pc0lkZW50aWZpZXIobmdNb2R1bGVWYWx1ZSkgJiYgIXRzLmlzUHJvcGVydHlBY2Nlc3NFeHByZXNzaW9uKG5nTW9kdWxlVmFsdWUpKSB7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG5cbiAgICBjb25zdCBuZ01vZHVsZURlY2xhcmF0aW9uID0gdGhpcy5nZXREZWNsYXJhdGlvbk9mRXhwcmVzc2lvbihuZ01vZHVsZVZhbHVlKTtcbiAgICBpZiAoIW5nTW9kdWxlRGVjbGFyYXRpb24pIHtcbiAgICAgIHRocm93IG5ldyBFcnJvcihcbiAgICAgICAgICBgQ2Fubm90IGZpbmQgYSBkZWNsYXJhdGlvbiBmb3IgTmdNb2R1bGUgJHtuZ01vZHVsZVZhbHVlLmdldFRleHQoKX0gcmVmZXJlbmNlZCBpbiBcIiR7ZGVjbGFyYXRpb24hLmdldFRleHQoKX1cImApO1xuICAgIH1cbiAgICBpZiAoIWhhc05hbWVJZGVudGlmaWVyKG5nTW9kdWxlRGVjbGFyYXRpb24ubm9kZSkpIHtcbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cbiAgICByZXR1cm4ge1xuICAgICAgbmFtZSxcbiAgICAgIG5nTW9kdWxlOiBuZ01vZHVsZURlY2xhcmF0aW9uIGFzIERlY2xhcmF0aW9uPENsYXNzRGVjbGFyYXRpb24+LCBkZWNsYXJhdGlvbiwgY29udGFpbmVyXG4gICAgfTtcbiAgfVxuXG4gIHByb3RlY3RlZCBnZXREZWNsYXJhdGlvbk9mRXhwcmVzc2lvbihleHByZXNzaW9uOiB0cy5FeHByZXNzaW9uKTogRGVjbGFyYXRpb258bnVsbCB7XG4gICAgaWYgKHRzLmlzSWRlbnRpZmllcihleHByZXNzaW9uKSkge1xuICAgICAgcmV0dXJuIHRoaXMuZ2V0RGVjbGFyYXRpb25PZklkZW50aWZpZXIoZXhwcmVzc2lvbik7XG4gICAgfVxuXG4gICAgaWYgKCF0cy5pc1Byb3BlcnR5QWNjZXNzRXhwcmVzc2lvbihleHByZXNzaW9uKSB8fCAhdHMuaXNJZGVudGlmaWVyKGV4cHJlc3Npb24uZXhwcmVzc2lvbikpIHtcbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cblxuICAgIGNvbnN0IG5hbWVzcGFjZURlY2wgPSB0aGlzLmdldERlY2xhcmF0aW9uT2ZJZGVudGlmaWVyKGV4cHJlc3Npb24uZXhwcmVzc2lvbik7XG4gICAgaWYgKCFuYW1lc3BhY2VEZWNsIHx8ICF0cy5pc1NvdXJjZUZpbGUobmFtZXNwYWNlRGVjbC5ub2RlKSkge1xuICAgICAgcmV0dXJuIG51bGw7XG4gICAgfVxuXG4gICAgY29uc3QgbmFtZXNwYWNlRXhwb3J0cyA9IHRoaXMuZ2V0RXhwb3J0c09mTW9kdWxlKG5hbWVzcGFjZURlY2wubm9kZSk7XG4gICAgaWYgKG5hbWVzcGFjZUV4cG9ydHMgPT09IG51bGwpIHtcbiAgICAgIHJldHVybiBudWxsO1xuICAgIH1cblxuICAgIGlmICghbmFtZXNwYWNlRXhwb3J0cy5oYXMoZXhwcmVzc2lvbi5uYW1lLnRleHQpKSB7XG4gICAgICByZXR1cm4gbnVsbDtcbiAgICB9XG5cbiAgICBjb25zdCBleHBvcnREZWNsID0gbmFtZXNwYWNlRXhwb3J0cy5nZXQoZXhwcmVzc2lvbi5uYW1lLnRleHQpICE7XG4gICAgcmV0dXJuIHsuLi5leHBvcnREZWNsLCB2aWFNb2R1bGU6IG5hbWVzcGFjZURlY2wudmlhTW9kdWxlfTtcbiAgfVxufVxuXG4vLy8vLy8vLy8vLy8vIEV4cG9ydGVkIEhlbHBlcnMgLy8vLy8vLy8vLy8vL1xuXG5leHBvcnQgdHlwZSBQYXJhbUluZm8gPSB7XG4gIGRlY29yYXRvcnM6IERlY29yYXRvcltdIHwgbnVsbCxcbiAgdHlwZUV4cHJlc3Npb246IHRzLkV4cHJlc3Npb24gfCBudWxsXG59O1xuXG4vKipcbiAqIEEgc3RhdGVtZW50IG5vZGUgdGhhdCByZXByZXNlbnRzIGFuIGFzc2lnbm1lbnQuXG4gKi9cbmV4cG9ydCB0eXBlIEFzc2lnbm1lbnRTdGF0ZW1lbnQgPVxuICAgIHRzLkV4cHJlc3Npb25TdGF0ZW1lbnQgJiB7ZXhwcmVzc2lvbjoge2xlZnQ6IHRzLklkZW50aWZpZXIsIHJpZ2h0OiB0cy5FeHByZXNzaW9ufX07XG5cbi8qKlxuICogVGVzdCB3aGV0aGVyIGEgc3RhdGVtZW50IG5vZGUgaXMgYW4gYXNzaWdubWVudCBzdGF0ZW1lbnQuXG4gKiBAcGFyYW0gc3RhdGVtZW50IHRoZSBzdGF0ZW1lbnQgdG8gdGVzdC5cbiAqL1xuZXhwb3J0IGZ1bmN0aW9uIGlzQXNzaWdubWVudFN0YXRlbWVudChzdGF0ZW1lbnQ6IHRzLlN0YXRlbWVudCk6IHN0YXRlbWVudCBpcyBBc3NpZ25tZW50U3RhdGVtZW50IHtcbiAgcmV0dXJuIHRzLmlzRXhwcmVzc2lvblN0YXRlbWVudChzdGF0ZW1lbnQpICYmIGlzQXNzaWdubWVudChzdGF0ZW1lbnQuZXhwcmVzc2lvbikgJiZcbiAgICAgIHRzLmlzSWRlbnRpZmllcihzdGF0ZW1lbnQuZXhwcmVzc2lvbi5sZWZ0KTtcbn1cblxuZXhwb3J0IGZ1bmN0aW9uIGlzQXNzaWdubWVudChub2RlOiB0cy5Ob2RlKTogbm9kZSBpcyB0cy5Bc3NpZ25tZW50RXhwcmVzc2lvbjx0cy5FcXVhbHNUb2tlbj4ge1xuICByZXR1cm4gdHMuaXNCaW5hcnlFeHByZXNzaW9uKG5vZGUpICYmIG5vZGUub3BlcmF0b3JUb2tlbi5raW5kID09PSB0cy5TeW50YXhLaW5kLkVxdWFsc1Rva2VuO1xufVxuXG4vKipcbiAqIFRoZSB0eXBlIG9mIGEgZnVuY3Rpb24gdGhhdCBjYW4gYmUgdXNlZCB0byBmaWx0ZXIgb3V0IGhlbHBlcnMgYmFzZWQgb24gdGhlaXIgdGFyZ2V0LlxuICogVGhpcyBpcyB1c2VkIGluIGByZWZsZWN0RGVjb3JhdG9yc0Zyb21IZWxwZXJDYWxsKClgLlxuICovXG5leHBvcnQgdHlwZSBUYXJnZXRGaWx0ZXIgPSAodGFyZ2V0OiB0cy5FeHByZXNzaW9uKSA9PiBib29sZWFuO1xuXG4vKipcbiAqIENyZWF0ZXMgYSBmdW5jdGlvbiB0aGF0IHRlc3RzIHdoZXRoZXIgdGhlIGdpdmVuIGV4cHJlc3Npb24gaXMgYSBjbGFzcyB0YXJnZXQuXG4gKiBAcGFyYW0gY2xhc3NOYW1lIHRoZSBuYW1lIG9mIHRoZSBjbGFzcyB3ZSB3YW50IHRvIHRhcmdldC5cbiAqL1xuZXhwb3J0IGZ1bmN0aW9uIG1ha2VDbGFzc1RhcmdldEZpbHRlcihjbGFzc05hbWU6IHN0cmluZyk6IFRhcmdldEZpbHRlciB7XG4gIHJldHVybiAodGFyZ2V0OiB0cy5FeHByZXNzaW9uKTogYm9vbGVhbiA9PiB0cy5pc0lkZW50aWZpZXIodGFyZ2V0KSAmJiB0YXJnZXQudGV4dCA9PT0gY2xhc3NOYW1lO1xufVxuXG4vKipcbiAqIENyZWF0ZXMgYSBmdW5jdGlvbiB0aGF0IHRlc3RzIHdoZXRoZXIgdGhlIGdpdmVuIGV4cHJlc3Npb24gaXMgYSBjbGFzcyBtZW1iZXIgdGFyZ2V0LlxuICogQHBhcmFtIGNsYXNzTmFtZSB0aGUgbmFtZSBvZiB0aGUgY2xhc3Mgd2Ugd2FudCB0byB0YXJnZXQuXG4gKi9cbmV4cG9ydCBmdW5jdGlvbiBtYWtlTWVtYmVyVGFyZ2V0RmlsdGVyKGNsYXNzTmFtZTogc3RyaW5nKTogVGFyZ2V0RmlsdGVyIHtcbiAgcmV0dXJuICh0YXJnZXQ6IHRzLkV4cHJlc3Npb24pOiBib29sZWFuID0+IHRzLmlzUHJvcGVydHlBY2Nlc3NFeHByZXNzaW9uKHRhcmdldCkgJiZcbiAgICAgIHRzLmlzSWRlbnRpZmllcih0YXJnZXQuZXhwcmVzc2lvbikgJiYgdGFyZ2V0LmV4cHJlc3Npb24udGV4dCA9PT0gY2xhc3NOYW1lICYmXG4gICAgICB0YXJnZXQubmFtZS50ZXh0ID09PSAncHJvdG90eXBlJztcbn1cblxuLyoqXG4gKiBIZWxwZXIgbWV0aG9kIHRvIGV4dHJhY3QgdGhlIHZhbHVlIG9mIGEgcHJvcGVydHkgZ2l2ZW4gdGhlIHByb3BlcnR5J3MgXCJzeW1ib2xcIixcbiAqIHdoaWNoIGlzIGFjdHVhbGx5IHRoZSBzeW1ib2wgb2YgdGhlIGlkZW50aWZpZXIgb2YgdGhlIHByb3BlcnR5LlxuICovXG5leHBvcnQgZnVuY3Rpb24gZ2V0UHJvcGVydHlWYWx1ZUZyb21TeW1ib2wocHJvcFN5bWJvbDogdHMuU3ltYm9sKTogdHMuRXhwcmVzc2lvbnx1bmRlZmluZWQge1xuICBjb25zdCBwcm9wSWRlbnRpZmllciA9IHByb3BTeW1ib2wudmFsdWVEZWNsYXJhdGlvbjtcbiAgY29uc3QgcGFyZW50ID0gcHJvcElkZW50aWZpZXIgJiYgcHJvcElkZW50aWZpZXIucGFyZW50O1xuICByZXR1cm4gcGFyZW50ICYmIHRzLmlzQmluYXJ5RXhwcmVzc2lvbihwYXJlbnQpID8gcGFyZW50LnJpZ2h0IDogdW5kZWZpbmVkO1xufVxuXG4vKipcbiAqIEEgY2FsbGVlIGNvdWxkIGJlIG9uZSBvZjogYF9fZGVjb3JhdGUoLi4uKWAgb3IgYHRzbGliXzEuX19kZWNvcmF0ZWAuXG4gKi9cbmZ1bmN0aW9uIGdldENhbGxlZU5hbWUoY2FsbDogdHMuQ2FsbEV4cHJlc3Npb24pOiBzdHJpbmd8bnVsbCB7XG4gIGlmICh0cy5pc0lkZW50aWZpZXIoY2FsbC5leHByZXNzaW9uKSkge1xuICAgIHJldHVybiBjYWxsLmV4cHJlc3Npb24udGV4dDtcbiAgfVxuICBpZiAodHMuaXNQcm9wZXJ0eUFjY2Vzc0V4cHJlc3Npb24oY2FsbC5leHByZXNzaW9uKSkge1xuICAgIHJldHVybiBjYWxsLmV4cHJlc3Npb24ubmFtZS50ZXh0O1xuICB9XG4gIHJldHVybiBudWxsO1xufVxuXG4vLy8vLy8vLy8vLy8vIEludGVybmFsIEhlbHBlcnMgLy8vLy8vLy8vLy8vL1xuXG4vKipcbiAqIEluIEVTMjAxNSwgYSBjbGFzcyBtYXkgYmUgZGVjbGFyZWQgdXNpbmcgYSB2YXJpYWJsZSBkZWNsYXJhdGlvbiBvZiB0aGUgZm9sbG93aW5nIHN0cnVjdHVyZTpcbiAqXG4gKiBgYGBcbiAqIHZhciBNeUNsYXNzID0gTXlDbGFzc18xID0gY2xhc3MgTXlDbGFzcyB7fTtcbiAqIGBgYFxuICpcbiAqIEhlcmUsIHRoZSBpbnRlcm1lZGlhdGUgYE15Q2xhc3NfMWAgYXNzaWdubWVudCBpcyBvcHRpb25hbC4gSW4gdGhlIGFib3ZlIGV4YW1wbGUsIHRoZVxuICogYGNsYXNzIE15Q2xhc3Mge31gIGV4cHJlc3Npb24gaXMgcmV0dXJuZWQgYXMgZGVjbGFyYXRpb24gb2YgYE15Q2xhc3NgLiBOb3RlIHRoYXQgaWYgYG5vZGVgXG4gKiByZXByZXNlbnRzIGEgcmVndWxhciBjbGFzcyBkZWNsYXJhdGlvbiwgaXQgd2lsbCBiZSByZXR1cm5lZCBhcy1pcy5cbiAqXG4gKiBAcGFyYW0gbm9kZSB0aGUgbm9kZSB0aGF0IHJlcHJlc2VudHMgdGhlIGNsYXNzIHdob3NlIGRlY2xhcmF0aW9uIHdlIGFyZSBmaW5kaW5nLlxuICogQHJldHVybnMgdGhlIGRlY2xhcmF0aW9uIG9mIHRoZSBjbGFzcyBvciBgbnVsbGAgaWYgaXQgaXMgbm90IGEgXCJjbGFzc1wiLlxuICovXG5mdW5jdGlvbiBnZXRJbm5lckNsYXNzRGVjbGFyYXRpb24obm9kZTogdHMuTm9kZSk6XG4gICAgQ2xhc3NEZWNsYXJhdGlvbjx0cy5DbGFzc0RlY2xhcmF0aW9ufHRzLkNsYXNzRXhwcmVzc2lvbj58bnVsbCB7XG4gIC8vIFJlY29nbml6ZSBhIHZhcmlhYmxlIGRlY2xhcmF0aW9uIG9mIHRoZSBmb3JtIGB2YXIgTXlDbGFzcyA9IGNsYXNzIE15Q2xhc3Mge31gIG9yXG4gIC8vIGB2YXIgTXlDbGFzcyA9IE15Q2xhc3NfMSA9IGNsYXNzIE15Q2xhc3Mge307YFxuICBpZiAodHMuaXNWYXJpYWJsZURlY2xhcmF0aW9uKG5vZGUpICYmIG5vZGUuaW5pdGlhbGl6ZXIgIT09IHVuZGVmaW5lZCkge1xuICAgIG5vZGUgPSBub2RlLmluaXRpYWxpemVyO1xuICAgIHdoaWxlIChpc0Fzc2lnbm1lbnQobm9kZSkpIHtcbiAgICAgIG5vZGUgPSBub2RlLnJpZ2h0O1xuICAgIH1cbiAgfVxuXG4gIGlmICghdHMuaXNDbGFzc0RlY2xhcmF0aW9uKG5vZGUpICYmICF0cy5pc0NsYXNzRXhwcmVzc2lvbihub2RlKSkge1xuICAgIHJldHVybiBudWxsO1xuICB9XG5cbiAgcmV0dXJuIGhhc05hbWVJZGVudGlmaWVyKG5vZGUpID8gbm9kZSA6IG51bGw7XG59XG5cbmZ1bmN0aW9uIGdldERlY29yYXRvckFyZ3Mobm9kZTogdHMuT2JqZWN0TGl0ZXJhbEV4cHJlc3Npb24pOiB0cy5FeHByZXNzaW9uW10ge1xuICAvLyBUaGUgYXJndW1lbnRzIG9mIGEgZGVjb3JhdG9yIGFyZSBoZWxkIGluIHRoZSBgYXJnc2AgcHJvcGVydHkgb2YgaXRzIGRlY2xhcmF0aW9uIG9iamVjdC5cbiAgY29uc3QgYXJnc1Byb3BlcnR5ID0gbm9kZS5wcm9wZXJ0aWVzLmZpbHRlcih0cy5pc1Byb3BlcnR5QXNzaWdubWVudClcbiAgICAgICAgICAgICAgICAgICAgICAgICAgIC5maW5kKHByb3BlcnR5ID0+IGdldE5hbWVUZXh0KHByb3BlcnR5Lm5hbWUpID09PSAnYXJncycpO1xuICBjb25zdCBhcmdzRXhwcmVzc2lvbiA9IGFyZ3NQcm9wZXJ0eSAmJiBhcmdzUHJvcGVydHkuaW5pdGlhbGl6ZXI7XG4gIHJldHVybiBhcmdzRXhwcmVzc2lvbiAmJiB0cy5pc0FycmF5TGl0ZXJhbEV4cHJlc3Npb24oYXJnc0V4cHJlc3Npb24pID9cbiAgICAgIEFycmF5LmZyb20oYXJnc0V4cHJlc3Npb24uZWxlbWVudHMpIDpcbiAgICAgIFtdO1xufVxuXG5mdW5jdGlvbiBpc1Byb3BlcnR5QWNjZXNzKG5vZGU6IHRzLk5vZGUpOiBub2RlIGlzIHRzLlByb3BlcnR5QWNjZXNzRXhwcmVzc2lvbiZcbiAgICB7cGFyZW50OiB0cy5CaW5hcnlFeHByZXNzaW9ufSB7XG4gIHJldHVybiAhIW5vZGUucGFyZW50ICYmIHRzLmlzQmluYXJ5RXhwcmVzc2lvbihub2RlLnBhcmVudCkgJiYgdHMuaXNQcm9wZXJ0eUFjY2Vzc0V4cHJlc3Npb24obm9kZSk7XG59XG5cbmZ1bmN0aW9uIGlzVGhpc0Fzc2lnbm1lbnQobm9kZTogdHMuRGVjbGFyYXRpb24pOiBub2RlIGlzIHRzLkJpbmFyeUV4cHJlc3Npb24mXG4gICAge2xlZnQ6IHRzLlByb3BlcnR5QWNjZXNzRXhwcmVzc2lvbn0ge1xuICByZXR1cm4gdHMuaXNCaW5hcnlFeHByZXNzaW9uKG5vZGUpICYmIHRzLmlzUHJvcGVydHlBY2Nlc3NFeHByZXNzaW9uKG5vZGUubGVmdCkgJiZcbiAgICAgIG5vZGUubGVmdC5leHByZXNzaW9uLmtpbmQgPT09IHRzLlN5bnRheEtpbmQuVGhpc0tleXdvcmQ7XG59XG5cbmZ1bmN0aW9uIGlzTmFtZWREZWNsYXJhdGlvbihub2RlOiB0cy5EZWNsYXJhdGlvbik6IG5vZGUgaXMgdHMuTmFtZWREZWNsYXJhdGlvbiZcbiAgICB7bmFtZTogdHMuSWRlbnRpZmllcn0ge1xuICBjb25zdCBhbnlOb2RlOiBhbnkgPSBub2RlO1xuICByZXR1cm4gISFhbnlOb2RlLm5hbWUgJiYgdHMuaXNJZGVudGlmaWVyKGFueU5vZGUubmFtZSk7XG59XG5cblxuZnVuY3Rpb24gaXNDbGFzc01lbWJlclR5cGUobm9kZTogdHMuRGVjbGFyYXRpb24pOiBub2RlIGlzIHRzLkNsYXNzRWxlbWVudHxcbiAgICB0cy5Qcm9wZXJ0eUFjY2Vzc0V4cHJlc3Npb258dHMuQmluYXJ5RXhwcmVzc2lvbiB7XG4gIHJldHVybiB0cy5pc0NsYXNzRWxlbWVudChub2RlKSB8fCBpc1Byb3BlcnR5QWNjZXNzKG5vZGUpIHx8IHRzLmlzQmluYXJ5RXhwcmVzc2lvbihub2RlKTtcbn1cblxuLyoqXG4gKiBDb2xsZWN0IG1hcHBpbmdzIGJldHdlZW4gZXhwb3J0ZWQgZGVjbGFyYXRpb25zIGluIGEgc291cmNlIGZpbGUgYW5kIGl0cyBhc3NvY2lhdGVkXG4gKiBkZWNsYXJhdGlvbiBpbiB0aGUgdHlwaW5ncyBwcm9ncmFtLlxuICovXG5mdW5jdGlvbiBjb2xsZWN0RXhwb3J0ZWREZWNsYXJhdGlvbnMoXG4gICAgY2hlY2tlcjogdHMuVHlwZUNoZWNrZXIsIGR0c0RlY2xhcmF0aW9uTWFwOiBNYXA8c3RyaW5nLCB0cy5EZWNsYXJhdGlvbj4sXG4gICAgc3JjRmlsZTogdHMuU291cmNlRmlsZSk6IHZvaWQge1xuICBjb25zdCBzcmNNb2R1bGUgPSBzcmNGaWxlICYmIGNoZWNrZXIuZ2V0U3ltYm9sQXRMb2NhdGlvbihzcmNGaWxlKTtcbiAgY29uc3QgbW9kdWxlRXhwb3J0cyA9IHNyY01vZHVsZSAmJiBjaGVja2VyLmdldEV4cG9ydHNPZk1vZHVsZShzcmNNb2R1bGUpO1xuICBpZiAobW9kdWxlRXhwb3J0cykge1xuICAgIG1vZHVsZUV4cG9ydHMuZm9yRWFjaChleHBvcnRlZFN5bWJvbCA9PiB7XG4gICAgICBpZiAoZXhwb3J0ZWRTeW1ib2wuZmxhZ3MgJiB0cy5TeW1ib2xGbGFncy5BbGlhcykge1xuICAgICAgICBleHBvcnRlZFN5bWJvbCA9IGNoZWNrZXIuZ2V0QWxpYXNlZFN5bWJvbChleHBvcnRlZFN5bWJvbCk7XG4gICAgICB9XG4gICAgICBjb25zdCBkZWNsYXJhdGlvbiA9IGV4cG9ydGVkU3ltYm9sLnZhbHVlRGVjbGFyYXRpb247XG4gICAgICBjb25zdCBuYW1lID0gZXhwb3J0ZWRTeW1ib2wubmFtZTtcbiAgICAgIGlmIChkZWNsYXJhdGlvbiAmJiAhZHRzRGVjbGFyYXRpb25NYXAuaGFzKG5hbWUpKSB7XG4gICAgICAgIGR0c0RlY2xhcmF0aW9uTWFwLnNldChuYW1lLCBkZWNsYXJhdGlvbik7XG4gICAgICB9XG4gICAgfSk7XG4gIH1cbn1cblxuLyoqXG4gKiBBdHRlbXB0IHRvIHJlc29sdmUgdGhlIHZhcmlhYmxlIGRlY2xhcmF0aW9uIHRoYXQgdGhlIGdpdmVuIGRlY2xhcmF0aW9uIGlzIGFzc2lnbmVkIHRvLlxuICogRm9yIGV4YW1wbGUsIGZvciB0aGUgZm9sbG93aW5nIGNvZGU6XG4gKlxuICogYGBgXG4gKiB2YXIgTXlDbGFzcyA9IE15Q2xhc3NfMSA9IGNsYXNzIE15Q2xhc3Mge307XG4gKiBgYGBcbiAqXG4gKiBhbmQgdGhlIHByb3ZpZGVkIGRlY2xhcmF0aW9uIGJlaW5nIGBjbGFzcyBNeUNsYXNzIHt9YCwgdGhpcyB3aWxsIHJldHVybiB0aGUgYHZhciBNeUNsYXNzYFxuICogZGVjbGFyYXRpb24uXG4gKlxuICogQHBhcmFtIGRlY2xhcmF0aW9uIFRoZSBkZWNsYXJhdGlvbiBmb3Igd2hpY2ggYW55IHZhcmlhYmxlIGRlY2xhcmF0aW9uIHNob3VsZCBiZSBvYnRhaW5lZC5cbiAqIEByZXR1cm5zIHRoZSBvdXRlciB2YXJpYWJsZSBkZWNsYXJhdGlvbiBpZiBmb3VuZCwgdW5kZWZpbmVkIG90aGVyd2lzZS5cbiAqL1xuZnVuY3Rpb24gZ2V0VmFyaWFibGVEZWNsYXJhdGlvbk9mRGVjbGFyYXRpb24oZGVjbGFyYXRpb246IHRzLkRlY2xhcmF0aW9uKTogdHMuVmFyaWFibGVEZWNsYXJhdGlvbnxcbiAgICB1bmRlZmluZWQge1xuICBsZXQgbm9kZSA9IGRlY2xhcmF0aW9uLnBhcmVudDtcblxuICAvLyBEZXRlY3QgYW4gaW50ZXJtZWRpYXJ5IHZhcmlhYmxlIGFzc2lnbm1lbnQgYW5kIHNraXAgb3ZlciBpdC5cbiAgaWYgKGlzQXNzaWdubWVudChub2RlKSAmJiB0cy5pc0lkZW50aWZpZXIobm9kZS5sZWZ0KSkge1xuICAgIG5vZGUgPSBub2RlLnBhcmVudDtcbiAgfVxuXG4gIHJldHVybiB0cy5pc1ZhcmlhYmxlRGVjbGFyYXRpb24obm9kZSkgPyBub2RlIDogdW5kZWZpbmVkO1xufVxuXG4vKipcbiAqIEEgY29uc3RydWN0b3IgZnVuY3Rpb24gbWF5IGhhdmUgYmVlbiBcInN5bnRoZXNpemVkXCIgYnkgVHlwZVNjcmlwdCBkdXJpbmcgSmF2YVNjcmlwdCBlbWl0LFxuICogaW4gdGhlIGNhc2Ugbm8gdXNlci1kZWZpbmVkIGNvbnN0cnVjdG9yIGV4aXN0cyBhbmQgZS5nLiBwcm9wZXJ0eSBpbml0aWFsaXplcnMgYXJlIHVzZWQuXG4gKiBUaG9zZSBpbml0aWFsaXplcnMgbmVlZCB0byBiZSBlbWl0dGVkIGludG8gYSBjb25zdHJ1Y3RvciBpbiBKYXZhU2NyaXB0LCBzbyB0aGUgVHlwZVNjcmlwdFxuICogY29tcGlsZXIgZ2VuZXJhdGVzIGEgc3ludGhldGljIGNvbnN0cnVjdG9yLlxuICpcbiAqIFdlIG5lZWQgdG8gaWRlbnRpZnkgc3VjaCBjb25zdHJ1Y3RvcnMgYXMgbmdjYyBuZWVkcyB0byBiZSBhYmxlIHRvIHRlbGwgaWYgYSBjbGFzcyBkaWRcbiAqIG9yaWdpbmFsbHkgaGF2ZSBhIGNvbnN0cnVjdG9yIGluIHRoZSBUeXBlU2NyaXB0IHNvdXJjZS4gV2hlbiBhIGNsYXNzIGhhcyBhIHN1cGVyY2xhc3MsXG4gKiBhIHN5bnRoZXNpemVkIGNvbnN0cnVjdG9yIG11c3Qgbm90IGJlIGNvbnNpZGVyZWQgYXMgYSB1c2VyLWRlZmluZWQgY29uc3RydWN0b3IgYXMgdGhhdFxuICogcHJldmVudHMgYSBiYXNlIGZhY3RvcnkgY2FsbCBmcm9tIGJlaW5nIGNyZWF0ZWQgYnkgbmd0c2MsIHJlc3VsdGluZyBpbiBhIGZhY3RvcnkgZnVuY3Rpb25cbiAqIHRoYXQgZG9lcyBub3QgaW5qZWN0IHRoZSBkZXBlbmRlbmNpZXMgb2YgdGhlIHN1cGVyY2xhc3MuIEhlbmNlLCB3ZSBpZGVudGlmeSBhIGRlZmF1bHRcbiAqIHN5bnRoZXNpemVkIHN1cGVyIGNhbGwgaW4gdGhlIGNvbnN0cnVjdG9yIGJvZHksIGFjY29yZGluZyB0byB0aGUgc3RydWN0dXJlIHRoYXQgVHlwZVNjcmlwdFxuICogZW1pdHMgZHVyaW5nIEphdmFTY3JpcHQgZW1pdDpcbiAqIGh0dHBzOi8vZ2l0aHViLmNvbS9NaWNyb3NvZnQvVHlwZVNjcmlwdC9ibG9iL3YzLjIuMi9zcmMvY29tcGlsZXIvdHJhbnNmb3JtZXJzL3RzLnRzI0wxMDY4LUwxMDgyXG4gKlxuICogQHBhcmFtIGNvbnN0cnVjdG9yIGEgY29uc3RydWN0b3IgZnVuY3Rpb24gdG8gdGVzdFxuICogQHJldHVybnMgdHJ1ZSBpZiB0aGUgY29uc3RydWN0b3IgYXBwZWFycyB0byBoYXZlIGJlZW4gc3ludGhlc2l6ZWRcbiAqL1xuZnVuY3Rpb24gaXNTeW50aGVzaXplZENvbnN0cnVjdG9yKGNvbnN0cnVjdG9yOiB0cy5Db25zdHJ1Y3RvckRlY2xhcmF0aW9uKTogYm9vbGVhbiB7XG4gIGlmICghY29uc3RydWN0b3IuYm9keSkgcmV0dXJuIGZhbHNlO1xuXG4gIGNvbnN0IGZpcnN0U3RhdGVtZW50ID0gY29uc3RydWN0b3IuYm9keS5zdGF0ZW1lbnRzWzBdO1xuICBpZiAoIWZpcnN0U3RhdGVtZW50IHx8ICF0cy5pc0V4cHJlc3Npb25TdGF0ZW1lbnQoZmlyc3RTdGF0ZW1lbnQpKSByZXR1cm4gZmFsc2U7XG5cbiAgcmV0dXJuIGlzU3ludGhlc2l6ZWRTdXBlckNhbGwoZmlyc3RTdGF0ZW1lbnQuZXhwcmVzc2lvbik7XG59XG5cbi8qKlxuICogVGVzdHMgd2hldGhlciB0aGUgZXhwcmVzc2lvbiBhcHBlYXJzIHRvIGhhdmUgYmVlbiBzeW50aGVzaXplZCBieSBUeXBlU2NyaXB0LCBpLmUuIHdoZXRoZXJcbiAqIGl0IGlzIG9mIHRoZSBmb2xsb3dpbmcgZm9ybTpcbiAqXG4gKiBgYGBcbiAqIHN1cGVyKC4uLmFyZ3VtZW50cyk7XG4gKiBgYGBcbiAqXG4gKiBAcGFyYW0gZXhwcmVzc2lvbiB0aGUgZXhwcmVzc2lvbiB0aGF0IGlzIHRvIGJlIHRlc3RlZFxuICogQHJldHVybnMgdHJ1ZSBpZiB0aGUgZXhwcmVzc2lvbiBhcHBlYXJzIHRvIGJlIGEgc3ludGhlc2l6ZWQgc3VwZXIgY2FsbFxuICovXG5mdW5jdGlvbiBpc1N5bnRoZXNpemVkU3VwZXJDYWxsKGV4cHJlc3Npb246IHRzLkV4cHJlc3Npb24pOiBib29sZWFuIHtcbiAgaWYgKCF0cy5pc0NhbGxFeHByZXNzaW9uKGV4cHJlc3Npb24pKSByZXR1cm4gZmFsc2U7XG4gIGlmIChleHByZXNzaW9uLmV4cHJlc3Npb24ua2luZCAhPT0gdHMuU3ludGF4S2luZC5TdXBlcktleXdvcmQpIHJldHVybiBmYWxzZTtcbiAgaWYgKGV4cHJlc3Npb24uYXJndW1lbnRzLmxlbmd0aCAhPT0gMSkgcmV0dXJuIGZhbHNlO1xuXG4gIGNvbnN0IGFyZ3VtZW50ID0gZXhwcmVzc2lvbi5hcmd1bWVudHNbMF07XG4gIHJldHVybiB0cy5pc1NwcmVhZEVsZW1lbnQoYXJndW1lbnQpICYmIHRzLmlzSWRlbnRpZmllcihhcmd1bWVudC5leHByZXNzaW9uKSAmJlxuICAgICAgYXJndW1lbnQuZXhwcmVzc2lvbi50ZXh0ID09PSAnYXJndW1lbnRzJztcbn1cbiJdfQ==