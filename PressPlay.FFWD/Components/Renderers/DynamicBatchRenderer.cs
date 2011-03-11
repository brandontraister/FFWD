﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PressPlay.FFWD.SkinnedModel;

namespace PressPlay.FFWD.Components
{
    public class DynamicBatchRenderer
    {
        public DynamicBatchRenderer(GraphicsDevice device)
        {
            this.device = device;
        }

        private Material currentMaterial = Material.Default;

        private GraphicsDevice device;

        private int batchVertexSize = 0;
        private int batchIndexSize = 0;
        private int currentVertexIndex = 0;
        private int currentIndexIndex = 0;

        private VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[20000];
        private Microsoft.Xna.Framework.Vector3[] positionData = new Microsoft.Xna.Framework.Vector3[600];
        private short[] indexData = new short[60000];

        /// <summary>
        /// Batch draw the current mesh filter.
        /// We will only do the actual draw call if the material has changed.
        /// Until that we will collect the mesh filters into a list and draw them all together.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal int Draw<T>(Camera cam, Material material, T verts, Transform transform)
        {
            int drawCalls = 0;

            if (currentMaterial.name != material.name)
            {
                drawCalls = DoDraw(device, cam);
                currentMaterial = material;
            }            
            Add(verts, transform);
            return drawCalls;
        }

        private void EndBatch()
        {
            currentMaterial = Material.Default;
            batchVertexSize = 0;
            batchIndexSize = 0;
            currentVertexIndex = 0;
            currentIndexIndex = 0;
        }

        private void Add<T>(T model, Transform transform)
        {
            Matrix world = (transform != null) ? transform.world : Matrix.Identity;
            MeshFilter filter = model as MeshFilter;
            if (filter != null)
            {
                PrepareMesh(filter.mesh, ref world);
            }

            Mesh mesh = model as Mesh;
            if (mesh != null)
            {
                PrepareMesh(mesh, ref world);
            }
        }

        internal int DoDraw(GraphicsDevice device, Camera cam)
        {
            if (currentIndexIndex == 0)
            {
                return 0;
            }

            cam.BasicEffect.World = Matrix.Identity;
            cam.BasicEffect.VertexColorEnabled = false;

            currentMaterial.SetTextureState(cam.BasicEffect);
            currentMaterial.SetBlendState(device);

#if DEBUG
            if (Camera.logRenderCalls)
            {
                Debug.LogFormat("Dyn batch draw: {0} on {1} verts {2}, indices {3}", currentMaterial.mainTexture, cam.gameObject, currentVertexIndex, currentIndexIndex);
            }
#endif

            foreach (EffectPass pass in cam.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    vertexData,
                    0,
                    currentVertexIndex,
                    indexData,
                    0,
                    currentIndexIndex / 3
                );
            }

            EndBatch();
            return 1;
        }

        private void PrepareMesh(Mesh mesh, ref Matrix transform)
        {
            batchVertexSize += mesh.vertices.Length;
            if (vertexData.Length < batchVertexSize)
            {
                VertexPositionNormalTexture[] newVertexData = new VertexPositionNormalTexture[batchVertexSize];
                vertexData.CopyTo(newVertexData, 0);
                vertexData = newVertexData;
#if DEBUG
                Debug.LogWarning("Increased size of Dynamic vertex buffer to " + batchVertexSize);
#endif
            }
            batchIndexSize += mesh.triangles.Length;
            if (indexData.Length < batchIndexSize)
            {
                short[] newIndexData = new short[batchIndexSize];
                indexData.CopyTo(newIndexData, 0);
                indexData = newIndexData;
#if DEBUG
                Debug.LogWarning("Increased size of Dynamic index buffer to " + batchIndexSize);
#endif
            }

            if (positionData.Length < mesh.vertices.Length)
            {
                positionData = new Microsoft.Xna.Framework.Vector3[mesh.vertices.Length];
#if DEBUG
                Debug.LogWarning("Increased size of Dynamic position buffer to " + mesh.vertices.Length);
#endif
            }

            if (transform != Matrix.Identity)
            {
                Microsoft.Xna.Framework.Vector3.Transform(mesh.vertices, ref transform, positionData);
            }
            else
            {
                mesh.vertices.CopyTo(positionData, 0);
            }

            for (int v = 0; v < mesh.vertices.Length; v++)
            {
                vertexData[currentVertexIndex + v].Position = positionData[v];
                if (mesh.uv != null)
                {
                    vertexData[currentVertexIndex + v].TextureCoordinate = mesh.uv[v];
                }
                else
                {
                    vertexData[currentVertexIndex + v].TextureCoordinate = Microsoft.Xna.Framework.Vector2.Zero;
                }
                if (mesh.normals != null)
                {
                    vertexData[currentVertexIndex + v].Normal = mesh.normals[v];
                }
                else
                {
                    vertexData[currentVertexIndex + v].Normal = Microsoft.Xna.Framework.Vector3.Zero;
                }
            }

            for (int t = 0; t < mesh.triangles.Length; t++)
            {
                indexData[currentIndexIndex + t] = (short)(mesh.triangles[t] + currentVertexIndex);
            }

            currentVertexIndex += mesh.vertices.Length;
            currentIndexIndex += mesh.triangles.Length;
        }
    }
}
