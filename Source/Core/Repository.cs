﻿/******************************************************************************
  Copyright 2009-2021 dataweb GmbH
  This file is part of the NShape framework.
  NShape is free software: you can redistribute it and/or modify it under the 
  terms of the GNU General Public License as published by the Free Software 
  Foundation, either version 3 of the License, or (at your option) any later 
  version.
  NShape is distributed in the hope that it will be useful, but WITHOUT ANY
  WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR 
  A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
  You should have received a copy of the GNU General Public License along with 
  NShape. If not, see <http://www.gnu.org/licenses/>.
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

using Dataweb.NShape.Advanced;


namespace Dataweb.NShape {

	#region IRepository Interface

	/// <summary>
	/// Defines the contract for storing NShape projects.
	/// </summary>
	public interface IRepository {

		/// <summary>
		/// Specifies the base storage format version.
		/// </summary>
		/// <description>This version describes the general storage format, e.g. for entities that 
		/// are not implemented in shape libraries.</description>
		int Version { get; set; }

		/// <summary>
		/// Specifies whether the repository's load/save version can be modified.
		/// </summary>
		bool CanModifyVersion { get; }

		/// <summary>
		/// Specifies the project for this repository. Null if not set.
		/// </summary>
		string ProjectName { get; set; }

		/// <summary>
		/// Registers an entity with the repository.
		/// </summary>
		/// <param name="entityType">Entity type names must be unique ignoring their casing.</param>
		void AddEntityType(IEntityType entityType);

		/// <summary>
		/// Unregisters an entity with the repository.
		/// </summary>
		void RemoveEntityType(string entityTypeName);

		/// <summary>
		/// Removes all registered entity types.
		/// </summary>
		/// <remarks>
		/// This method must be called before different libraries are loaded and their entities re-registered.
		/// </remarks>
		void RemoveAllEntityTypes();

		/// <summary>
		/// Indicates whether the project exists in the persistent store of the repository.
		/// </summary>
		/// <returns>True, wenn the repository is connected to an existing persistent store.</returns>
		bool Exists();

		/// <summary>
		/// Reads the version number of the project from the persistent store.
		/// </summary>
		void ReadVersion();

		/// <summary>
		/// Creates and opens a new project in the repository.
		/// </summary>
		/// <remarks>
		/// Create does not actually create the repository. We want to give the client 
		/// a chance to not flush it and thereby not having performed any durable action.
		/// </remarks>
		void Create();

		/// <summary>
		/// Opens an existing project in the repository.
		/// </summary>
		void Open();

		/// <summary>
		/// Closes the repository.
		/// </summary>
		void Close();

		/// <summary>
		/// Deletes the persistent store of the project from the repository.
		/// </summary>
		void Erase();

		/// <summary>
		/// Indicates, whether the reposistory is open.
		/// </summary>
		bool IsOpen { get; }

		/// <summary>
		/// Indicates, whether modifications have been performed since the last call
		/// to Open or SaveChanges.
		/// </summary>
		bool IsModified { get; }

		/// <summary>
		/// Gets a bottom z-order value for the given diagram.
		/// </summary>
		/// <param name="diagram"></param>
		/// <returns></returns>
		int ObtainNewBottomZOrder(Diagram diagram);

		/// <summary>
		/// Gets a top z-order value for the given diagram.
		/// </summary>
		/// <param name="diagram"></param>
		/// <returns></returns>
		int ObtainNewTopZOrder(Diagram diagram);

		/// <summary>
		/// Submits all modifications in the repository to the data store.
		/// </summary>
		void SaveChanges();

		/// <summary>
		/// Checks whether the given shape type is referenced by any shapes in the project. 
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsShapeTypeInUse(ShapeType shapeType);

		/// <summary>
		/// Checks whether the given shape types are referenced by any shapes in the project. 
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsShapeTypeInUse(IEnumerable<ShapeType> shapeType);

		/// <summary>
		/// Checks whether the given model object type is referenced by any model object in the project. 
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsModelObjectTypeInUse(ModelObjectType shapeType);

		/// <summary>
		/// Checks whether the given model object type is referenced by any model object in the project. 
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsModelObjectTypeInUse(IEnumerable<ModelObjectType> shapeType);


		#region Project

		/// <summary>
		/// Retrieves the current project.
		/// </summary>
		/// <returns></returns>
		ProjectSettings GetProject();

		/// <summary>
		/// Updates the current project.
		/// </summary>
		void Update();

		/// <summary>
		/// Deletes the current project.
		/// </summary>
		void Delete();

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryProjectEventArgs> ProjectUpdated;

		#endregion


		#region Designs

		/// <summary>
		/// Retrieves all known designs.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Design> GetDesigns();

		/// <summary>
		/// Fetches a single design object from the repository.
		/// </summary>
		/// <param name="id">Id of design to fetch. Null to indicate the project design.</param>
		/// <returns>Reference to object</returns>
		Design GetDesign(object id);

		/// <summary>
		/// Fetches a single design object from the repository.
		/// </summary>
		/// <param name="name">Name of the design to fetch.</param>
		/// <returns>Reference to object</returns>
		Design GetDesign(string name);

		/// <summary>
		/// Inserts a new design into the repository.
		/// </summary>
		void Insert(Design design);

		/// <summary>
		/// Inserts a new design and all its styles into the repository.
		/// </summary>
		void InsertAll(Design design);

		/// <summary>
		/// Updates the given design. The design's styles are not affected.
		/// </summary>
		/// <param name="design"></param>
		void Update(Design design);

		/// <summary>
		/// Deletes the given design from the repository.
		/// </summary>
		/// <param name="design"></param>
		void Delete(Design design);

		/// <summary>
		/// Deletes the given design and all its styles from the repository.
		/// </summary>
		/// <param name="design"></param>
		void DeleteAll(Design design);

		/// <summary>
		/// Undeletes the given design.
		/// </summary>
		void Undelete(Design design);

		/// <summary>
		/// Undeletes the given design and all its styles.
		/// </summary>
		void UndeleteAll(Design design);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDesignEventArgs> DesignInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDesignEventArgs> DesignUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDesignEventArgs> DesignDeleted;

		#endregion


		#region Styles

		/// <summary>
		/// Checks whether the given style is referenced by any styles or shapes in the project. 
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsStyleInUse(IStyle style);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(Design design, IStyle style);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IStyle style);

		/// <summary>
		/// Deletes the given <see cref="T:Dataweb.NShape.IStyle"/> from the repository.
		/// </summary>
		/// <remarks>
		/// This method does NOT check if the <see cref="T:Dataweb.NShape.IStyle"/> is still in use. 
		/// Use <see cref="M:Dataweb.NShape.IRepository.IsStyleInUse"/> to check whether the <see cref="T:Dataweb.NShape.IStyle"/> is in use before deleting it.
		/// </remarks>
		void Delete(IStyle style);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(Design design, IStyle style);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryStyleEventArgs> StyleInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryStyleEventArgs> StyleUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryStyleEventArgs> StyleDeleted;

		#endregion


		#region Model

		/// <summary>
		/// Retrieves the current model.
		/// Throws an exception if no model exists. Use ModelExists method to check.
		/// </summary>
		Model GetModel();

		/// <summary>
		/// Inserts a new model.
		/// </summary>
		void Insert(Model model);

		/// <summary>
		/// Updates the current model.
		/// </summary>
		void Update(Model model);

		/// <summary>
		/// Deletes the current model.
		/// </summary>
		void Delete(Model model);

		/// <summary>
		/// Undeletes the current model.
		/// </summary>
		void Undelete(Model model);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelEventArgs> ModelInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelEventArgs> ModelUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelEventArgs> ModelDeleted;

		#endregion


		#region ModelObjects

		/// <summary>
		/// Checks whether the given model object is referenced by any shape or child model object in any diagram of the project.
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsModelObjectInUse(IModelObject modelObject);

		/// <summary>
		/// Fetches a single object from the repository.
		/// </summary>
		/// <param name="id">Id of object to fetch</param>
		/// <returns>Reference to object or null, if object was not found.</returns>
		IModelObject GetModelObject(object id);

		/// <ToBeCompleted></ToBeCompleted>
		IEnumerable<IModelObject> GetModelObjects(IModelObject parent);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IModelObject modelObject, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IModelObject modelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IEnumerable<IModelObject> modelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IModelObject modelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IEnumerable<IModelObject> modelObjects);

		/// <summary>
		/// Updates the model object's owner, which is now a <see cref="T:Dataweb.NShape.Advanced.IModelObject" />.
		/// </summary>
		void UpdateOwner(IModelObject modelObject, IModelObject parent);

		/// <summary>
		/// Updates the model object's owner, which is now a <see cref="T:Dataweb.NShape.Advanced.Template" />.
		/// </summary>
		void UpdateOwner(IModelObject modelObject, Template template);

		/// <summary>
		/// Updates the model object's owner, which is now the <see cref="T:Dataweb.NShape.Advanced.Model" />.
		/// </summary>
		void UpdateOwner(IModelObject modelObject, Model model);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IModelObject modelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IEnumerable<IModelObject> modelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IModelObject modelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IEnumerable<IModelObject> modelObjects);

		/// <summary>
		/// Removes the given model objects from the repository.
		/// </summary>
		/// <param name="modelObjects"></param>
		void Unload(IEnumerable<IModelObject> modelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelObjectsEventArgs> ModelObjectsInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelObjectsEventArgs> ModelObjectsUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryModelObjectsEventArgs> ModelObjectsDeleted;

		#endregion


		#region DiagramModelObjects

		/// <summary>
		/// Checks whether the given diagram model object is referenced by any diagram.
		/// If the repository's store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsDiagramModelObjectInUse(IDiagramModelObject diagramModelObject);

		/// <summary>
		/// Fetches a single object from the repository.
		/// </summary>
		/// <param name="id">Id of object to fetch</param>
		/// <returns>Reference to object or null, if object was not found.</returns>
		IDiagramModelObject GetDiagramModelObject(object id);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IDiagramModelObject diagramModelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IEnumerable<IDiagramModelObject> diagramModelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IDiagramModelObject diagramModelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IEnumerable<IDiagramModelObject> diagramModelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IDiagramModelObject diagramModelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IEnumerable<IDiagramModelObject> diagramModelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IDiagramModelObject diagramModelObject);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IEnumerable<IDiagramModelObject> diagramModelObjects);

		/// <summary>
		/// Removes the given diagram model objects from the repository.
		/// </summary>
		/// <param name="diagramModelObjects"></param>
		void Unload(IEnumerable<IDiagramModelObject> diagramModelObjects);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramModelObjectsEventArgs> DiagramModelObjectsInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramModelObjectsEventArgs> DiagramModelObjectsUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramModelObjectsEventArgs> DiagramModelObjectsDeleted;

		#endregion


		#region Templates

		/// <summary>
		/// Checks whether the given template is referenced by any shape in any diagram of the project.
		/// If the repositories store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsTemplateInUse(Template template);

		/// <summary>
		/// Checks if one of the given templates is referenced by any shape in any diagram of the project.
		/// If the repositories store supports partial loading, the store performs the same checks without loading the corresponding objects.
		/// </summary>
		bool IsTemplateInUse(IEnumerable<Template> templates);

		/// <summary>
		/// Fetches a single object from the repository.
		/// </summary>
		/// <param name="id">Id of object to fetch</param>
		/// <returns>Reference to object or null, if object was not found.</returns>
		Template GetTemplate(object id);

		/// <summary>
		/// Fetches a single template given its name.
		/// </summary>
		Template GetTemplate(string name);

		/// <summary>
		/// Fetches all <see cref="T:Dataweb.NShape.Advanced.Template" /> in the project from the repository.
		/// </summary>
		/// <returns>Iterator to step through the <see cref="T:Dataweb.NShape.Advanced.Template" /> list.</returns>
		IEnumerable<Template> GetTemplates();

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void InsertAll(Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(Template template);

		///// <ToBeCompleted></ToBeCompleted>
		//void UpdateAll(Template template);

		/// <summary>
		/// Replaces the shape of the template.
		/// </summary>
		void ReplaceTemplateShape(Template template, Shape oldShape, Shape newShape);

		/// <summary>
		/// Deletes the given <see cref="T:Dataweb.NShape.Advanced.Template"/> from the repository.
		/// </summary>
		/// <remarks>
		/// This method does NOT delete the template's shape or model object.
		/// This method does NOT check if the template is still in use. 
		/// Use method IsTemplateInUse to check whether the template is in use before deleting it.
		/// </remarks>
		void Delete(Template template);

		/// <summary>
		/// Deletes the given <see cref="T:Dataweb.NShape.Advanced.Template"/> from the repository.
		/// </summary>
		/// <remarks>
		/// This method does NOT check if the template is still in use. 
		/// Use method IsTemplateInUse to check whether the template is in use before deleting it.
		/// </remarks>
		void DeleteAll(Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void UndeleteAll(Template template);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> TemplateInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> TemplateUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> TemplateDeleted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateShapeReplacedEventArgs> TemplateShapeReplaced;

		#endregion


		#region ModelMappings

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IModelMapping modelMapping, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IEnumerable<IModelMapping> modelMappings, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IModelMapping modelMapping);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IEnumerable<IModelMapping> modelMappings);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IModelMapping modelMapping);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IEnumerable<IModelMapping> modelMappings);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IModelMapping modelMapping, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IEnumerable<IModelMapping> modelMappings, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> ModelMappingsInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> ModelMappingsUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryTemplateEventArgs> ModelMappingsDeleted;

		#endregion


		#region Diagrams

		/// <summary>
		/// Fetches a single diagram object from the repository.
		/// </summary>
		/// <param name="id">Id of object to fetch</param>
		/// <returns>Reference to object or null, if object was not found.</returns>
		/// <remarks>
		/// If the store supports partial loading, the diagram shapes are not loaded. 
		/// Use GetDiagramShapes for loading the diagram's content.
		/// </remarks>
		Diagram GetDiagram(object id);

		/// <summary>
		/// Fetches a single diagram identified by its name.
		/// </summary>
		/// <remarks>
		/// If the store supports partial loading, the diagram shapes are not loaded. 
		/// Use GetDiagramShapes for loading the diagram's content.
		/// </remarks>
		Diagram GetDiagram(string name);

		/// <summary>
		/// Returns all the diagrams of this project.
		/// </summary>
		/// <remarks>
		/// If the store supports partial loading, the diagram shapes are not loaded. 
		/// Use GetDiagramShapes for loading the diagram's content.
		/// </remarks>
		IEnumerable<Diagram> GetDiagrams();

		/// <summary>
		/// Inserts the given diagram into the repository.
		/// </summary>
		void Insert(Diagram diagram);

		/// <summary>
		/// Inserts the given diagram and all its shapes (including shape connections) into the repository.
		/// </summary>
		void InsertAll(Diagram diagram);

		/// <summary>
		/// Updates the given diagram. The diagram's shapes are not affected.
		/// </summary>
		void Update(Diagram diagram);

		/// <summary>
		/// Deletes the given diagram from the repository.
		/// </summary>
		void Delete(Diagram diagram);

		/// <summary>
		/// Deletes the given diagram and all its shapes from the repository.
		/// </summary>
		void DeleteAll(Diagram diagram);

		/// <summary>
		/// Undeletes the given diagram.
		/// </summary>
		/// <param name="diagram"></param>
		void Undelete(Diagram diagram);

		/// <summary>
		/// Undeletes the given diagram and all its shapes.
		/// </summary>
		void UndeleteAll(Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramEventArgs> DiagramInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramEventArgs> DiagramUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryDiagramEventArgs> DiagramDeleted;

		#endregion


		#region Shapes

		/// <summary>
		/// Makes sure that the shapes for the given diagram that intersect with one 
		/// of the given rectangles are loaded.
		/// </summary>
		/// <remarks>Partial loading via rectangles parameter is not supported yet. Currently alle 
		/// shapes of the diagram are loaded.</remarks>
		void GetDiagramShapes(Diagram diagram, params Rectangle[] rectangles);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(Shape shape, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(Shape shape, Shape parentShape);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(Shape shape, Template owningTemplate);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert<TShape>(TShape shape, Diagram diagram) where TShape : Shape;

		/// <ToBeCompleted></ToBeCompleted>
		void Insert<TShape>(TShape shape, Shape parentShape) where TShape : Shape;

		/// <ToBeCompleted></ToBeCompleted>
		void Insert<TShape>(TShape shape, Template owningTemplate) where TShape : Shape;

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IEnumerable<Shape> shapes, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void Insert(IEnumerable<Shape> shapes, Shape parentShape);

		/// <summary>
		/// Inserts the given shape in the repository. The owner will be the given diagram.
		/// </summary>
		/// <remarks>If the shape has a model object assigned, the model object will NOT be inserted!</remarks>
		void InsertAll(Shape shape, Diagram diagram);

		/// <summary>
		/// Inserts the given shape in the repository. The owner will be the given parent shape.
		/// </summary>
		/// <remarks>If the shape has a model object assigned, the model object will NOT be inserted!</remarks>
		void InsertAll(Shape shape, Shape parentShape);

		/// <summary>
		/// Inserts the given shape in the repository. The owner will be the given template.
		/// </summary>
		/// <remarks>If the shape has a model object assigned, the model object will NOT be inserted!</remarks>
		void InsertAll(Shape shape, Template owningTemplate);

		/// <summary>
		/// Inserts the given shapes in the repository. The owner of each shape will be the given diagram.
		/// </summary>
		/// <remarks>Model objects assigned to shapes will NOT be inserted!</remarks>
		void InsertAll(IEnumerable<Shape> shapes, Diagram diagram);

		/// <summary>
		/// Inserts the given shapes in the repository. The owner of each shape will be the given parent shape.
		/// </summary>
		/// <remarks>Model objects assigned to shapes will NOT be inserted!</remarks>
		void InsertAll(IEnumerable<Shape> shapes, Shape parentShape);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(Shape shape);

		/// <ToBeCompleted></ToBeCompleted>
		void Update<TShape>(TShape shape) where TShape : Shape;

		///// <ToBeCompleted></ToBeCompleted>
		//void UpdateAll(Shape shape);

		/// <summary>
		/// Updates the shape's parent, which is now a <see cref="T:Dataweb.NShape.Diagram" />.
		/// </summary>
		/// <param name="shape"><see cref="T:Dataweb.NShape.Advanced.Shape" /> whose parent has changed</param>
		/// <param name="diagram">New parent of the <see cref="T:Dataweb.NShape.Advanced.Shape" /></param>
		void UpdateOwner(Shape shape, Diagram diagram);

		/// <summary>
		/// Updates the shape's parent, which is now a <see cref="T:Dataweb.NShape.Advanced.Shape" />.
		/// </summary>
		/// <param name="shape"><see cref="T:Dataweb.NShape.Advanced.Shape" /> whose parent has changed</param>
		/// <param name="parent">New parent of the <see cref="T:Dataweb.NShape.Advanced.Shape" /></param>
		void UpdateOwner(Shape shape, Shape parent);

		/// <ToBeCompleted></ToBeCompleted>
		void Update(IEnumerable<Shape> shapes);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(Shape shape);

		/// <ToBeCompleted></ToBeCompleted>
		void Delete(IEnumerable<Shape> shapes);

		/// <ToBeCompleted></ToBeCompleted>
		void DeleteAll(Shape shape);

		/// <ToBeCompleted></ToBeCompleted>
		void DeleteAll(IEnumerable<Shape> shapes);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(Shape shape, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(Shape shape, Template template);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IEnumerable<Shape> shapes, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(Shape shape, Shape parent);

		/// <ToBeCompleted></ToBeCompleted>
		void Undelete(IEnumerable<Shape> shapes, Shape parent);

		/// <ToBeCompleted></ToBeCompleted>
		void UndeleteAll(Shape shape, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void UndeleteAll(Shape shape, Shape parent);

		/// <ToBeCompleted></ToBeCompleted>
		void UndeleteAll(IEnumerable<Shape> shapes, Diagram diagram);

		/// <ToBeCompleted></ToBeCompleted>
		void UndeleteAll(IEnumerable<Shape> shapes, Shape parent);

		/// <summary>
		/// Removes all shapes of the diagram from the repository.
		/// </summary>
		/// <param name="diagram"></param>
		void Unload(Diagram diagram);

		/// <summary>
		/// Inserts a new shape connection into the repository.
		/// </summary>
		void InsertConnection(Shape activeShape, ControlPointId gluePointId, Shape passiveShape, ControlPointId connectionPointId);

		/// <summary>
		/// Deletes a shape connection from the repository.
		/// </summary>
		void DeleteConnection(Shape activeShape, ControlPointId gluePointId, Shape passiveShape, ControlPointId connectionPointId);

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryShapesEventArgs> ShapesInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryShapesEventArgs> ShapesUpdated;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryShapesEventArgs> ShapesDeleted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryShapeConnectionEventArgs> ConnectionInserted;

		/// <ToBeCompleted></ToBeCompleted>
		event EventHandler<RepositoryShapeConnectionEventArgs> ConnectionDeleted;

		#endregion

	}

	#endregion


	#region Repository EventArgs

	/// <summary>
	/// Encapsulates parameters for a project-related respository event.
	/// </summary>
	public class RepositoryProjectEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryProjectEventArgs(ProjectSettings projectSettings) {
			if (projectSettings == null) throw new ArgumentNullException("projectSettings");
			this._projectSettings = projectSettings;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public ProjectSettings Project {
			get { return _projectSettings; }
			internal set { _projectSettings = value; }
		}

		internal RepositoryProjectEventArgs() { }

		private ProjectSettings _projectSettings = null;
	}


	/// <summary>
	/// Encapsulates parameters for a project-related respository event.
	/// </summary>
	public class RepositoryModelEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryModelEventArgs(Model model) {
			if (model == null) throw new ArgumentNullException("model");
			this._model = model;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public Model Model {
			get { return _model; }
			internal set { _model = value; }
		}

		internal RepositoryModelEventArgs() { }

		private Model _model = null;
	}


	/// <summary>
	/// Encapsulates parameters for a design-related respository event.
	/// </summary>
	public class RepositoryDesignEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryDesignEventArgs(Design design) {
			if (design == null) throw new ArgumentNullException("design");
			this._design = design;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public Design Design {
			get { return _design; }
			internal set { _design = value; }
		}

		internal RepositoryDesignEventArgs() { }

		private Design _design = null;
	}


	/// <summary>
	/// Encapsulates parameters for a project-related respository event.
	/// </summary>
	public class RepositoryStyleEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryStyleEventArgs(IStyle style) {
			if (style == null) throw new ArgumentNullException("style");
			this._style = style;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public IStyle Style {
			get { return _style; }
			internal set { _style = value; }
		}

		internal RepositoryStyleEventArgs() { }

		private IStyle _style = null;
	}


	/// <summary>
	/// Encapsulates parameters for a diagram-related respository event.
	/// </summary>
	public class RepositoryDiagramEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryDiagramEventArgs(Diagram diagram) {
			if (diagram == null) throw new ArgumentNullException("diagram");
			this._diagram = diagram;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public Diagram Diagram {
			get { return _diagram; }
			internal set { _diagram = value; }
		}

		internal RepositoryDiagramEventArgs() { }

		private Diagram _diagram = null;
	}


	/// <summary>
	/// Encapsulates parameters for a template-related respository event.
	/// </summary>
	public class RepositoryTemplateEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryTemplateEventArgs(Template template) {
			if (template == null) throw new ArgumentNullException("template");
			this._template = template;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public Template Template {
			get { return _template; }
			internal set { _template = value; }
		}

		internal RepositoryTemplateEventArgs() { }

		private Template _template = null;
	}


	/// <summary>
	/// Encapsulates parameters for respository events raised when template shapes are exchanged.
	/// </summary>
	public class RepositoryTemplateShapeReplacedEventArgs : RepositoryTemplateEventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryTemplateShapeReplacedEventArgs(Template template, Shape oldTemplateShape, Shape newTemplateShape)
			: base(template) {
			if (oldTemplateShape == null) throw new ArgumentNullException("oldTemplateShape");
			if (newTemplateShape == null) throw new ArgumentNullException("newTemplateShape");
			this._oldTemplateShape = oldTemplateShape;
			this._newTemplateShape = newTemplateShape;
		}

		/// <ToBeCompleted></ToBeCompleted>
		public Shape OldTemplateShape {
			get { return _oldTemplateShape; }
			internal set { _oldTemplateShape = value; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public Shape NewTemplateShape {
			get { return _newTemplateShape; }
			internal set { _newTemplateShape = value; }
		}


		internal RepositoryTemplateShapeReplacedEventArgs() : base() { }

		private Shape _oldTemplateShape = null;
		private Shape _newTemplateShape = null;
	}


	/// <summary>
	/// Encapsulates parameters for a shape-related respository event.
	/// </summary>
	public class RepositoryShapesEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryShapesEventArgs(IEnumerable<Shape> shapes, Diagram diagram) {
			if (shapes == null) throw new ArgumentNullException("shapes");
			SetShapes(shapes, diagram);
		}


		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryShapesEventArgs(IEnumerable<KeyValuePair<Shape, Diagram>> shapesWithDiagrams) {
			if (shapesWithDiagrams == null) throw new ArgumentNullException("shapesWithDiagrams");
			_shapes.Clear();
			foreach (KeyValuePair<Shape, Diagram> item in shapesWithDiagrams)
				_shapes.Add(item.Key, item.Value);
		}


		/// <ToBeCompleted></ToBeCompleted>
		public IEnumerable<Shape> Shapes {
			get { return _shapes.Keys; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public Diagram GetDiagram(Shape s) {
			Diagram d;
			if (_shapes.TryGetValue(s, out d)) return d;
			else return null;
		}


		/// <ToBeCompleted></ToBeCompleted>
		public int Count {
			get { return _shapes.Count; }
		}


		internal RepositoryShapesEventArgs() {
			this._shapes.Clear();
		}


		internal void Clear() {
			_shapes.Clear();
		}


		internal void AddShape(Shape shape, Diagram diagram) {
			_shapes.Add(shape, diagram);
		}


		internal void SetShapes(IEnumerable<Shape> shapes, Diagram diagram) {
			this._shapes.Clear();
			foreach (Shape s in shapes) this._shapes.Add(s, diagram);
		}


		internal void SetShape(Shape shape, Diagram diagram) {
			this._shapes.Clear();
			this._shapes.Add(shape, diagram);
		}


		private Dictionary<Shape, Diagram> _shapes = new Dictionary<Shape, Diagram>();
	}


	/// <summary>
	/// Encapsulates parameters for a modelobject-related respository event.
	/// </summary>
	public class RepositoryModelObjectsEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryModelObjectsEventArgs(IEnumerable<IModelObject> modelObjects) {
			if (modelObjects == null) throw new ArgumentNullException("modelObjects");
			this._modelObjects.AddRange(modelObjects);
		}


		/// <ToBeCompleted></ToBeCompleted>
		public IEnumerable<IModelObject> ModelObjects {
			get { return _modelObjects; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public int Count {
			get { return _modelObjects.Count; }
		}


		internal RepositoryModelObjectsEventArgs() {
			_modelObjects.Clear();
		}


		internal void SetModelObjects(IEnumerable<IModelObject> modelObjects) {
			this._modelObjects.Clear();
			this._modelObjects.AddRange(modelObjects);
		}


		internal void SetModelObject(IModelObject modelObject) {
			_modelObjects.Clear();
			_modelObjects.Add(modelObject);
		}


		private List<IModelObject> _modelObjects = new List<IModelObject>();
	}


	/// <summary>
	/// Encapsulates parameters for a modelobject-related respository event.
	/// </summary>
	public class RepositoryDiagramModelObjectsEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryDiagramModelObjectsEventArgs(IEnumerable<IDiagramModelObject> diagramModelObjects) {
			if (diagramModelObjects == null) throw new ArgumentNullException("diagramModelObjects");
			this._diagramModelObjects.AddRange(diagramModelObjects);
		}


		/// <ToBeCompleted></ToBeCompleted>
		public IEnumerable<IDiagramModelObject> DiagramModelObjects {
			get { return _diagramModelObjects; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public int Count {
			get { return _diagramModelObjects.Count; }
		}


		internal RepositoryDiagramModelObjectsEventArgs() {
			_diagramModelObjects.Clear();
		}


		internal void SetModelObjects(IEnumerable<IDiagramModelObject> diagramModelObjects) {
			this._diagramModelObjects.Clear();
			this._diagramModelObjects.AddRange(diagramModelObjects);
		}


		internal void SetModelObject(IDiagramModelObject diagramModelObject) {
			_diagramModelObjects.Clear();
			_diagramModelObjects.Add(diagramModelObject);
		}


		private List<IDiagramModelObject> _diagramModelObjects = new List<IDiagramModelObject>();
	}


	/// <summary>
	/// Encapsulates parameters for a shape connection related respository event.
	/// </summary>
	public class RepositoryShapeConnectionEventArgs : EventArgs {

		/// <ToBeCompleted></ToBeCompleted>
		public RepositoryShapeConnectionEventArgs(Shape connectorShape, ControlPointId gluePointId, Shape targetShape, ControlPointId targetPointId)
			: this() {
			if (connectorShape == null) throw new ArgumentNullException("connectorShape");
			if (targetShape == null) throw new ArgumentNullException("targetShape");
			if (gluePointId == ControlPointId.Any || gluePointId == ControlPointId.None)
				throw new ArgumentException("gluePointId");
			if (!connectorShape.HasControlPointCapability(gluePointId, ControlPointCapabilities.Glue))
				throw new ArgumentException(string.Format(Dataweb.NShape.Properties.Resources.MessageFmt_0IsNotAGluePointOf1, gluePointId, connectorShape.Type.FullName));
			if (targetPointId == ControlPointId.Any || targetPointId == ControlPointId.None)
				throw new ArgumentException("targetPointId");
			if (!targetShape.HasControlPointCapability(targetPointId, ControlPointCapabilities.Connect))
				throw new ArgumentException(string.Format(Dataweb.NShape.Properties.Resources.MessageFmt_0IsNotAConnectionPointOf1, targetPointId, targetShape.Type.FullName));

			this._connection.ConnectorShape = connectorShape;
			this._connection.GluePointId = gluePointId;
			this._connection.TargetShape = targetShape;
			this._connection.TargetPointId = targetPointId;
		}


		/// <ToBeCompleted></ToBeCompleted>
		protected internal RepositoryShapeConnectionEventArgs(ShapeConnection shapeConnection)
			: this(shapeConnection.ConnectorShape, shapeConnection.GluePointId, shapeConnection.TargetShape, shapeConnection.TargetPointId) {
		}


		/// <ToBeCompleted></ToBeCompleted>
		protected internal RepositoryShapeConnectionEventArgs() {
			this._connection = ShapeConnection.Empty;
		}


		/// <ToBeCompleted></ToBeCompleted>
		public Shape ConnectorShape {
			get { return _connection.ConnectorShape; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public ControlPointId GluePointId {
			get { return _connection.GluePointId; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public Shape TargetShape {
			get { return _connection.TargetShape; }
		}


		/// <ToBeCompleted></ToBeCompleted>
		public ControlPointId TargetPointId {
			get { return _connection.TargetPointId; }
		}


		internal void Clear() {
			_connection = ShapeConnection.Empty;
		}


		internal void SetShapeConnection(ShapeConnection connection) {
			System.Diagnostics.Debug.Assert(connection != ShapeConnection.Empty);
			this._connection = connection;
		}


		private ShapeConnection _connection;
	}

	#endregion

}
