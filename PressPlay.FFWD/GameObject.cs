﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using PressPlay.FFWD.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Box2D.XNA;

namespace PressPlay.FFWD
{
    public class GameObject
    {
        public GameObject()
        {
            components = new List<Component>();
        }

        public int id { get; set; }
        public string name { get; set; }

        private Transform _transform;
        public Transform transform 
        { 
            get
            {
                return _transform;
            }
            set
            {
                _transform = value;
                _transform.gameObject = this;
            }
        }

        //public String prefab { get; set; }
        [ContentSerializer(CollectionItemName = "component")]
        private List<Component> components { get; set; }

        // TODO: We must export this as well
        [ContentSerializerIgnore]
        public bool active { get; set; }

        internal void AfterLoad()
        {
            for (int j = 0; j < components.Count; j++)
            {
                components[j].gameObject = this;
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].transform._parent = transform;
                    transform.children[i].AfterLoad();
                }
            }
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.gameObject = this;
        }

        #region Update and event methods
        internal void FixedUpdate()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (!components[i].isStarted)
                {
                    components[i].Start();
                    components[i].isStarted = true;
                }
                if (Component.IsAwake(components[i]) && components[i] is IFixedUpdateable)
                {
                    (components[i] as IFixedUpdateable).FixedUpdate();
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].FixedUpdate();
                }
            }
        }

        internal void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (!components[i].isStarted)
                {
                    components[i].Start();
                    components[i].isStarted = true;
                }
                if (Component.IsAwake(components[i]) && components[i] is IUpdateable)
                {
                    (components[i] as IUpdateable).Update();
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].Update();
                }
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is IRenderable)
                {
                    (components[i] as IRenderable).Draw(spriteBatch);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].Draw(spriteBatch);
                }
            }
        }

        internal void OnTriggerEnter(Contact contact)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnTriggerEnter(contact);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnTriggerEnter(contact);
                }
            }
        }

        internal void OnTriggerExit(Contact contact)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnTriggerExit(contact);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnTriggerExit(contact);
                }
            }
        }

        internal void OnCollisionEnter(Contact contact)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnCollisionEnter(contact);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnCollisionEnter(contact);
                }
            }
        }

        internal void OnCollisionExit(Contact contact)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnCollisionExit(contact);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnCollisionExit(contact);
                }
            }
        }

        internal void OnPreSolve(Contact contact, Manifold manifold)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnPreSolve(contact, manifold);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnPreSolve(contact, manifold);
                }
            }
        }

        internal void OnPostSolve(Contact contact, ContactImpulse contactImpulse)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (Component.IsAwake(components[i]) && components[i] is ICollidable)
                {
                    (components[i] as ICollidable).OnPostSolve(contact, contactImpulse);
                }
            }
            if (transform != null && transform.children != null)
            {
                for (int i = 0; i < transform.children.Count; i++)
                {
                    transform.children[i].OnPostSolve(contact, contactImpulse);
                }
            }
        }
        #endregion

        #region Component locator methods
        public T[] GetComponents<T>() where T : Component
        {
            List<T> list = new List<T>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                {
                    list.Add(components[i] as T);
                }
            }
            return list.ToArray();
        }

        public Component[] GetComponents(Type type)
        {
            List<Component> list = new List<Component>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(type))
                {
                    list.Add(components[i]);
                }
            }
            return list.ToArray();
        }

        public Component[] GetComponentsInChildren(Type type)
        {
            List<Component> list = new List<Component>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(type))
                {
                    list.Add(components[i]);
                }
            }
            if (transform.children != null)
            {
                for (int childIndex = 0; childIndex < transform.children.Count; childIndex++)
                {
                    list.AddRange(transform.children[childIndex].GetComponentsInChildren(type));
                }
            }
            return list.ToArray();
        }

        public Component GetComponentInChildren(Type type)
        {
            if (transform.children != null)
            {
                for (int childIndex = 0; childIndex < transform.children.Count; childIndex++)
                {
                    Component cmp = transform.children[childIndex].GetComponentInChildren(type);
                    if (cmp != null)
                    {
                        return cmp;
                    }
                }
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(type))
                {
                    return components[i];
                }
            }
            return null;
        }

        public T[] GetComponentsInParents<T>() where T : Component
        {
            List<T> list = new List<T>();
            GameObject go = GetParent();
            while (go != null)
            {
                list.AddRange(go.GetComponents<T>());
                go = go.GetParent();
            }
            return list.ToArray();
        }

        private GameObject GetParent()
        {
            return (transform != null && transform.parent != null) ? transform.parent.gameObject : null;
        }

        public static Component[] FindObjectsOfType(Type type)
        {
            List<Component> list = new List<Component>();
            if (Application.Instance.currentScene != null)
            {
                foreach (GameObject go in Application.Instance.currentScene.gameObjects)
                {
                    list.AddRange(go.GetComponentsInChildren(type));
                }
            }
            return list.ToArray();
        }

        public static Component FindObjectOfType(Type type)
        {
            Component cmp = null;
            if (Application.Instance.currentScene != null)
            {
                foreach (GameObject go in Application.Instance.currentScene.gameObjects)
                {
                    cmp = go.GetComponentInChildren(type);
                    if (cmp != null)
                    {
                        return cmp;
                    }
                }
            }
            return null;
        }
        #endregion

    }
}
