﻿using System;
using System.Text;
using Microsoft.Xna.Framework;

namespace PressPlay.FFWD
{
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        #region Public Fields

        public float m00;
        public float m10;
        public float m20;
        public float m30;

        public float m01;
        public float m11;
        public float m21;
        public float m31;
       
        public float m02;
        public float m12;
        public float m22;
        public float m32;

        public float m03;
        public float m13;
        public float m23;
        public float m33;
        
        #endregion Public Fields

        #region Properties

        public static Matrix4x4 identity
        {
            get
            {
                Matrix4x4 m;

                m.m00 = 1;
                m.m01 = 0;
                m.m02 = 0;
                m.m03 = 0;

                m.m10 = 0;
                m.m11 = 1;
                m.m12 = 0;
                m.m13 = 0;

                m.m20 = 0;
                m.m21 = 0;
                m.m22 = 1;
                m.m23 = 0;

                m.m30 = 0;
                m.m31 = 0;
                m.m32 = 0;
                m.m33 = 1;

                return m;
            }
        }

        public static Matrix4x4 zero
        {
            get
            {
                Matrix4x4 m;

                m.m00 = 0;
                m.m01 = 0;
                m.m02 = 0;
                m.m03 = 0;

                m.m10 = 0;
                m.m11 = 0;
                m.m12 = 0;
                m.m13 = 0;

                m.m20 = 0;
                m.m21 = 0;
                m.m22 = 0;
                m.m23 = 0;

                m.m30 = 0;
                m.m31 = 0;
                m.m32 = 0;
                m.m33 = 0;

                return m;
            }
        }

        public Matrix4x4 inverse
        {
            get
            {
                return Matrix4x4.Inverse(this);
            }
        }

        public bool isIdentity(Matrix4x4 m)
        {

            if (m.m00 == 1) && (m.m01 == 0) && (m.m02 == 0) && (m.m03 == 0)
                && (m.m10 == 0) && (m.m11 == 1) && (m.m12 == 0) && (m.m13 == 0)
                && (m.m20 == 0) && (m.m21 == 0) && (m.m22 == 1) && (m.m23 == 0)
                && (m.m30 == 0) && (m.m31 == 0) && (m.m32 == 0) && (m.m33 == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m00;
                    case 1:
                        return m10;
                    case 2:
                        return m20;
                    case 3:
                        return m30;
                    case 4:
                        return m01;
                    case 5:
                        return m11;
                    case 6:
                        return m21;
                    case 7:
                        return m31;
                    case 8:
                        return m02;
                    case 9:
                        return m12;
                    case 10:
                        return m22;
                    case 11:
                        return m32;
                    case 12:
                        return m03;
                    case 13:
                        return m13;
                    case 14:
                        return m23;
                    case 15:
                        return m33;
                    default:
                        throw new IndexOutOfRangeException("You must use an index between 0 and 15 to access the data");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m00 = value;
                        break;
                    case 1:
                        m10 = value;
                        break;
                    case 2:
                        m20 = value;
                        break;
                    case 3:
                        m30 = value;
                        break;
                    case 4:
                        m01 = value;
                        break;
                    case 5:
                        m11 = value;
                        break;
                    case 6:
                        m21 = value;
                        break;
                    case 7:
                        m31 = value;
                        break;
                    case 8:
                        m02 = value;
                        break;
                    case 9:
                        m12 = value;
                        break;
                    case 10:
                        m22 = value;
                        break;
                    case 11:
                        m32 = value;
                        break;
                    case 12:
                        m03 = value;
                        break;
                    case 13:
                        m13 = value;
                        break;
                    case 14:
                        m23 = value;
                        break;
                    case 15:
                        m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("You must use an index between 0 and 15 to access the data");
                }
            }
        }

        public float this[int row, int col]
        {
            get
            {
                return this[row + col * 4];
            }
            set
            {
                this[row + col * 4] = value;
            }
        }
        #endregion Properties

        #region Public Methods

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return (obj is Matrix4x4) ? this == (Matrix4x4)obj : false;
        }

        public bool Equals(Matrix4x4 other)
        {
            return this == other;
        }

        public Vector4 GetColumn(int i)
        {
            switch (i)
            {
                case 0:
                    return Vector4(m00,m01,m02,m03);
                case 1:
                    return Vector4(m10,m11,m12,m13);
                case 2:
                    return Vector4(m20,m21,m22,m23);
                case 3:
                    return Vector4(m30,m31,m32,m33);
                default:
                    throw new IndexOutOfRangeException("i must be from 0 to 3 inclusive.");
            }

        }

        public Vector4 GetRow(int i)
        {
            switch (i)
            {
                case 0:
                    return Vector4(m00,m10,m20,m30);
                case 1:
                    return Vector4(m01,m11,m21,m31);
                case 2:
                    return Vector4(m02,m12,m22,m32);
                case 3:
                    return Vector4(m03,m13,m23,m33);
                default:
                    throw new IndexOutOfRangeException("i must be from 0 to 3 inclusive.");
            }
        }

        public void SetColumn(int i, Vector4 v)
        {
            switch (i)
            {
                case 0:
                    m00 = v.x;
                    m01 = v.y;
                    m02 = v.z;
                    m03 = v.w;
                    break;
                case 1:
                    m10 = v.x;
                    m11 = v.y;
                    m12 = v.z;
                    m13 = v.w;
                    break;
                case 2:
                    m20 = v.x;
                    m21 = v.y;
                    m22 = v.z;
                    m23 = v.w;
                    break:
                case 3:
                    m30 = v.x;
                    m31 = v.y;
                    m32 = v.z;
                    m33 = v.w;
                    break:
                default:
                    throw new IndexOutOfRangeException("i must be from 0 to 3 inclusive.");
            }
        }

        public void SetRow(int i, Vector4 v)
        {
            switch (i)
            {
                case 0:
                    m00 = v.x;
                    m10 = v.y;
                    m20 = v.z;
                    m30 = v.w;
                    break;
                case 1:
                    m01 = v.x;
                    m11 = v.y;
                    m21 = v.z;
                    m31 = v.w;
                    break;
                case 2:
                    m02 = v.x;
                    m12 = v.y;
                    m22 = v.z;
                    m32 = v.w;
                    break:
                case 3:
                    m03 = v.x;
                    m13 = v.y;
                    m23 = v.z;
                    m33 = v.w;
                    break:
                default:
                    throw new IndexOutOfRangeException("i must be from 0 to 3 inclusive.");
            }
        }

        public Vector3 MultiplyPoint(Vector3 v)
        {
            Vector3 vReturn;

            vReturn.x = (((m00 * v.x) + (m01 * v.y)) + (m02 * v.z)) + m03;
            vReturn.y = (((m10 * v.x) + (m11 * v.y)) + (m12 * v.z)) + m13;
            vReturn.z = (((m20 * v.x) + (m21 * v.y)) + (m22 * v.z)) + m23;

            return vReturn;
        }

        public Vector3 MultiplyVector(Vector3 v)
        {
            Vector3 vReturn;

            vReturn.x = ((m00 * v.x) + (m01 * v.y)) + (m02 * v.z);
            vReturn.y = ((m10 * v.x) + (m11 * v.y)) + (m12 * v.z);
            vReturn.z = ((m20 * v.x) + (m21 * v.y)) + (m22 * v.z);

            return vReturn;
        }

        public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "{ {m00:" + m00 + " m01:" + m01 + " m02:" + m02 + " m03:" + m03 + "}" +
                    " {m10:" + m10 + " m11:" + m11 + " m12:" + m12 + " m13:" + m13 + "}" +
                    " {m20:" + m20 + " m21:" + m21 + " m22:" + m22 + " m23:" + m23 + "}" +
                    " {m30:" + m30 + " m31:" + m31 + " m32:" + m32 + " m33:" + m33 + "} }";
        }

        #endregion Public Methods

        #region Public Static Methods

        public static Matrix4x4 Inverse(Matrix4x4 matrix)
        {
            ///
            // Use Laplace expansion theorem to calculate the inverse of a 4x4 matrix
            // 
            // 1. Calculate the 2x2 determinants needed and the 4x4 determinant based on the 2x2 determinants 
            // 3. Create the adjugate matrix, which satisfies: A * adj(A) = det(A) * I
            // 4. Divide adjugate matrix with the determinant to find the inverse
            float det1 = matrix.m00 * matrix.m11 - matrix.m01 * matrix.m10;
            float det2 = matrix.m00 * matrix.m12 - matrix.m02 * matrix.m10;
            float det3 = matrix.m00 * matrix.m13 - matrix.m03 * matrix.m10;
            float det4 = matrix.m01 * matrix.m12 - matrix.m02 * matrix.m11;
            float det5 = matrix.m01 * matrix.m13 - matrix.m03 * matrix.m11;
            float det6 = matrix.m02 * matrix.m13 - matrix.m03 * matrix.m12;
            float det7 = matrix.m20 * matrix.m31 - matrix.m21 * matrix.m30;
            float det8 = matrix.m20 * matrix.m32 - matrix.m22 * matrix.m30;
            float det9 = matrix.m20 * matrix.m33 - matrix.m23 * matrix.m30;
            float det10 = matrix.m21 * matrix.m32 - matrix.m22 * matrix.m31;
            float det11 = matrix.m21 * matrix.m33 - matrix.m23 * matrix.m31;
            float det12 = matrix.m22 * matrix.m33 - matrix.m23 * matrix.m32;

            float detMatrix = (float)(det1 * det12 - det2 * det11 + det3 * det10 + det4 * det9 - det5 * det8 + det6 * det7);

            float invDetMatrix = 1f / detMatrix;

            Matrix4x4 ret; // Allow for matrix and result to point to the same structure

            ret.m00 = (matrix.m11 * det12 - matrix.m12 * det11 + matrix.m13 * det10) * invDetMatrix;
            ret.m01 = (-matrix.m01 * det12 + matrix.m02 * det11 - matrix.m03 * det10) * invDetMatrix;
            ret.m02 = (matrix.m31 * det6 - matrix.m32 * det5 + matrix.m33 * det4) * invDetMatrix;
            ret.m03 = (-matrix.m21 * det6 + matrix.m22 * det5 - matrix.m23 * det4) * invDetMatrix;
            ret.m10 = (-matrix.m10 * det12 + matrix.m12 * det9 - matrix.m13 * det8) * invDetMatrix;
            ret.m11 = (matrix.m00 * det12 - matrix.m02 * det9 + matrix.m03 * det8) * invDetMatrix;
            ret.m12 = (-matrix.m30 * det6 + matrix.m32 * det3 - matrix.m33 * det2) * invDetMatrix;
            ret.m13 = (matrix.m20 * det6 - matrix.m22 * det3 + matrix.m23 * det2) * invDetMatrix;
            ret.m20 = (matrix.m10 * det11 - matrix.m11 * det9 + matrix.m13 * det7) * invDetMatrix;
            ret.m21 = (-matrix.m00 * det11 + matrix.m01 * det9 - matrix.m03 * det7) * invDetMatrix;
            ret.m22 = (matrix.m30 * det5 - matrix.m31 * det3 + matrix.m33 * det1) * invDetMatrix;
            ret.m23 = (-matrix.m20 * det5 + matrix.m21 * det3 - matrix.m23 * det1) * invDetMatrix;
            ret.m30 = (-matrix.m10 * det10 + matrix.m11 * det8 - matrix.m12 * det7) * invDetMatrix;
            ret.m31 = (matrix.m00 * det10 - matrix.m01 * det8 + matrix.m02 * det7) * invDetMatrix;
            ret.m32 = (-matrix.m30 * det4 + matrix.m31 * det2 - matrix.m32 * det1) * invDetMatrix;
            ret.m33 = (matrix.m20 * det4 - matrix.m21 * det2 + matrix.m22 * det1) * invDetMatrix;

            return ret;
        }

        public static Matrix4x4 Transpose(Matrix4x4 matrix)
        {
            Matrix4x4 result;
            result.m00 = matrix.m00;
            result.m01 = matrix.m10;
            result.m02 = matrix.m20;
            result.m03 = matrix.m30;

            result.m10 = matrix.m01;
            result.m11 = matrix.m11;
            result.m12 = matrix.m21;
            result.m13 = matrix.m31;

            result.m20 = matrix.m02;
            result.m21 = matrix.m12;
            result.m22 = matrix.m22;
            result.m23 = matrix.m32;

            result.m30 = matrix.m03;
            result.m31 = matrix.m13;
            result.m32 = matrix.m23;
            result.m33 = matrix.m33;

            return result;
        }

        public static Matrix4x4 Scale(Vector3 v)
        {
            throw new NotImplementedException();
        }

        public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            throw new NotImplementedException();
        }

        public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
        {
            throw new NotImplementedException();
        }

        #endregion Public Static Methods

        #region Operators
        public static explicit operator Matrix(Matrix4x4 m)
        {
            return new Matrix(
                m.m00, m.m01, m.m02, m.m03,
                m.m10, m.m11, m.m12, m.m13,
                m.m20, m.m21, m.m22, m.m23,
                m.m30, m.m31, m.m32, m.m33);
        }

        public static explicit operator Matrix4x4(Matrix m)
        {
            return new Matrix4x4()
            {
                m00 = m.M11,
                m01 = m.M12,
                m02 = m.M13,
                m03 = m.M14,
                m10 = m.M21,
                m11 = m.M22,
                m12 = m.M23,
                m13 = m.M24,
                m20 = m.M31,
                m21 = m.M32,
                m22 = m.M33,
                m23 = m.M34,
                m30 = m.M41,
                m31 = m.M42,
                m32 = m.M43,
                m33 = m.M44
            };
        }

        public static bool operator ==(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            return (matrix1.m00 == matrix2.m00) && (matrix1.m01 == matrix2.m01) &&
                   (matrix1.m02 == matrix2.m02) && (matrix1.m03 == matrix2.m03) &&
                   (matrix1.m10 == matrix2.m10) && (matrix1.m11 == matrix2.m11) &&
                   (matrix1.m12 == matrix2.m12) && (matrix1.m13 == matrix2.m13) &&
                   (matrix1.m20 == matrix2.m20) && (matrix1.m21 == matrix2.m21) &&
                   (matrix1.m22 == matrix2.m22) && (matrix1.m23 == matrix2.m23) &&
                   (matrix1.m30 == matrix2.m30) && (matrix1.m31 == matrix2.m31) &&
                   (matrix1.m32 == matrix2.m32) && (matrix1.m33 == matrix2.m33);
        }

        public static bool operator !=(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            return !(matrix1 == matrix2);
        }

        public static Matrix4x4 operator *(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            Matrix4x4 result;

            result.m00 = matrix1.m00 * matrix2.m00 + matrix1.m01 * matrix2.m10 + matrix1.m02 * matrix2.m20 + matrix1.m03 * matrix2.m30;
            result.m01 = matrix1.m00 * matrix2.m01 + matrix1.m01 * matrix2.m11 + matrix1.m02 * matrix2.m21 + matrix1.m03 * matrix2.m31;
            result.m02 = matrix1.m00 * matrix2.m02 + matrix1.m01 * matrix2.m12 + matrix1.m02 * matrix2.m22 + matrix1.m03 * matrix2.m32;
            result.m03 = matrix1.m00 * matrix2.m03 + matrix1.m01 * matrix2.m13 + matrix1.m02 * matrix2.m23 + matrix1.m03 * matrix2.m33;

            result.m10 = matrix1.m10 * matrix2.m00 + matrix1.m11 * matrix2.m10 + matrix1.m12 * matrix2.m20 + matrix1.m13 * matrix2.m30;
            result.m11 = matrix1.m10 * matrix2.m01 + matrix1.m11 * matrix2.m11 + matrix1.m12 * matrix2.m21 + matrix1.m13 * matrix2.m31;
            result.m12 = matrix1.m10 * matrix2.m02 + matrix1.m11 * matrix2.m12 + matrix1.m12 * matrix2.m22 + matrix1.m13 * matrix2.m32;
            result.m13 = matrix1.m10 * matrix2.m03 + matrix1.m11 * matrix2.m13 + matrix1.m12 * matrix2.m23 + matrix1.m13 * matrix2.m33;

            result.m20 = matrix1.m20 * matrix2.m00 + matrix1.m21 * matrix2.m10 + matrix1.m22 * matrix2.m20 + matrix1.m23 * matrix2.m30;
            result.m21 = matrix1.m20 * matrix2.m01 + matrix1.m21 * matrix2.m11 + matrix1.m22 * matrix2.m21 + matrix1.m23 * matrix2.m31;
            result.m22 = matrix1.m20 * matrix2.m02 + matrix1.m21 * matrix2.m12 + matrix1.m22 * matrix2.m22 + matrix1.m23 * matrix2.m32;
            result.m23 = matrix1.m20 * matrix2.m03 + matrix1.m21 * matrix2.m13 + matrix1.m22 * matrix2.m23 + matrix1.m23 * matrix2.m33;

            result.m30 = matrix1.m30 * matrix2.m00 + matrix1.m31 * matrix2.m10 + matrix1.m32 * matrix2.m20 + matrix1.m33 * matrix2.m30;
            result.m31 = matrix1.m30 * matrix2.m01 + matrix1.m31 * matrix2.m11 + matrix1.m32 * matrix2.m21 + matrix1.m33 * matrix2.m31;
            result.m32 = matrix1.m30 * matrix2.m02 + matrix1.m31 * matrix2.m12 + matrix1.m32 * matrix2.m22 + matrix1.m33 * matrix2.m32;
            result.m33 = matrix1.m30 * matrix2.m03 + matrix1.m31 * matrix2.m13 + matrix1.m32 * matrix2.m23 + matrix1.m33 * matrix2.m33;

            return result;
        }

        public static Matrix4x4 operator *(Matrix4x4 matrix, float scaleFactor)
        {
            Matrix4x4 target;
            target.m00 = matrix.m00 * scaleFactor;
            target.m01 = matrix.m01 * scaleFactor;
            target.m02 = matrix.m02 * scaleFactor;
            target.m03 = matrix.m03 * scaleFactor;
            target.m10 = matrix.m10 * scaleFactor;
            target.m11 = matrix.m11 * scaleFactor;
            target.m12 = matrix.m12 * scaleFactor;
            target.m13 = matrix.m13 * scaleFactor;
            target.m20 = matrix.m20 * scaleFactor;
            target.m21 = matrix.m21 * scaleFactor;
            target.m22 = matrix.m22 * scaleFactor;
            target.m23 = matrix.m23 * scaleFactor;
            target.m30 = matrix.m30 * scaleFactor;
            target.m31 = matrix.m31 * scaleFactor;
            target.m32 = matrix.m32 * scaleFactor;
            target.m33 = matrix.m33 * scaleFactor;
            return target;
        }

        public static Matrix4x4 operator *(float scaleFactor, Matrix4x4 matrix)
        {
            Matrix4x4 target;
            target.m00 = matrix.m00 * scaleFactor;
            target.m01 = matrix.m01 * scaleFactor;
            target.m02 = matrix.m02 * scaleFactor;
            target.m03 = matrix.m03 * scaleFactor;
            target.m10 = matrix.m10 * scaleFactor;
            target.m11 = matrix.m11 * scaleFactor;
            target.m12 = matrix.m12 * scaleFactor;
            target.m13 = matrix.m13 * scaleFactor;
            target.m20 = matrix.m20 * scaleFactor;
            target.m21 = matrix.m21 * scaleFactor;
            target.m22 = matrix.m22 * scaleFactor;
            target.m23 = matrix.m23 * scaleFactor;
            target.m30 = matrix.m30 * scaleFactor;
            target.m31 = matrix.m31 * scaleFactor;
            target.m32 = matrix.m32 * scaleFactor;
            target.m33 = matrix.m33 * scaleFactor;
            return target;
        }

        public static Matrix4x4 operator /(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
             Matrix4x4 inverse = Matrix4x4.Invert(matrix2), result;

            result.m00 = matrix1.m00 * inverse.m00 + matrix1.m01 * inverse.m10 + matrix1.m02 * inverse.m20 + matrix1.m03 * inverse.m30;
            result.m01 = matrix1.m00 * inverse.m01 + matrix1.m01 * inverse.m11 + matrix1.m02 * inverse.m21 + matrix1.m03 * inverse.m31;
            result.m02 = matrix1.m00 * inverse.m02 + matrix1.m01 * inverse.m12 + matrix1.m02 * inverse.m22 + matrix1.m03 * inverse.m32;
            result.m03 = matrix1.m00 * inverse.m03 + matrix1.m01 * inverse.m13 + matrix1.m02 * inverse.m23 + matrix1.m03 * inverse.m33;

            result.m10 = matrix1.m10 * inverse.m00 + matrix1.m11 * inverse.m10 + matrix1.m12 * inverse.m20 + matrix1.m13 * inverse.m30;
            result.m11 = matrix1.m10 * inverse.m01 + matrix1.m11 * inverse.m11 + matrix1.m12 * inverse.m21 + matrix1.m13 * inverse.m31;
            result.m12 = matrix1.m10 * inverse.m02 + matrix1.m11 * inverse.m12 + matrix1.m12 * inverse.m22 + matrix1.m13 * inverse.m32;
            result.m13 = matrix1.m10 * inverse.m03 + matrix1.m11 * inverse.m13 + matrix1.m12 * inverse.m23 + matrix1.m13 * inverse.m33;

            result.m20 = matrix1.m20 * inverse.m00 + matrix1.m21 * inverse.m10 + matrix1.m22 * inverse.m20 + matrix1.m23 * inverse.m30;
            result.m21 = matrix1.m20 * inverse.m01 + matrix1.m21 * inverse.m11 + matrix1.m22 * inverse.m21 + matrix1.m23 * inverse.m31;
            result.m22 = matrix1.m20 * inverse.m02 + matrix1.m21 * inverse.m12 + matrix1.m22 * inverse.m22 + matrix1.m23 * inverse.m32;
            result.m23 = matrix1.m20 * inverse.m03 + matrix1.m21 * inverse.m13 + matrix1.m22 * inverse.m23 + matrix1.m23 * inverse.m33;

            result.m30 = matrix1.m30 * inverse.m00 + matrix1.m31 * inverse.m10 + matrix1.m32 * inverse.m20 + matrix1.m33 * inverse.m30;
            result.m31 = matrix1.m30 * inverse.m01 + matrix1.m31 * inverse.m11 + matrix1.m32 * inverse.m21 + matrix1.m33 * inverse.m31;
            result.m32 = matrix1.m30 * inverse.m02 + matrix1.m31 * inverse.m12 + matrix1.m32 * inverse.m22 + matrix1.m33 * inverse.m32;
            result.m33 = matrix1.m30 * inverse.m03 + matrix1.m31 * inverse.m13 + matrix1.m32 * inverse.m23 + matrix1.m33 * inverse.m33;

            return result;
        }

        public static Matrix4x4 operator /(Matrix4x4 matrix1, float divider)
        {
            float inverseDivider = 1.0f / divider;

            matrix1.m00 = matrix1.m00 * inverseDivider;
            matrix1.m01 = matrix1.m01 * inverseDivider;
            matrix1.m02 = matrix1.m02 * inverseDivider;
            matrix1.m03 = matrix1.m03 * inverseDivider;
            matrix1.m10 = matrix1.m10 * inverseDivider;
            matrix1.m11 = matrix1.m11 * inverseDivider;
            matrix1.m12 = matrix1.m12 * inverseDivider;
            matrix1.m13 = matrix1.m13 * inverseDivider;
            matrix1.m20 = matrix1.m20 * inverseDivider;
            matrix1.m21 = matrix1.m21 * inverseDivider;
            matrix1.m22 = matrix1.m22 * inverseDivider;
            matrix1.m23 = matrix1.m23 * inverseDivider;
            matrix1.m30 = matrix1.m30 * inverseDivider;
            matrix1.m31 = matrix1.m31 * inverseDivider;
            matrix1.m32 = matrix1.m32 * inverseDivider;
            matrix1.m33 = matrix1.m33 * inverseDivider;

            return matrix1;
        }

        public static Matrix4x4 operator +(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            matrix1.m00 += matrix2.m00;
            matrix1.m01 += matrix2.m01;
            matrix1.m02 += matrix2.m02;
            matrix1.m03 += matrix2.m03;
            matrix1.m10 += matrix2.m10;
            matrix1.m11 += matrix2.m11;
            matrix1.m12 += matrix2.m12;
            matrix1.m13 += matrix2.m13;
            matrix1.m20 += matrix2.m20;
            matrix1.m21 += matrix2.m21;
            matrix1.m22 += matrix2.m22;
            matrix1.m23 += matrix2.m23;
            matrix1.m30 += matrix2.m30;
            matrix1.m31 += matrix2.m31;
            matrix1.m32 += matrix2.m32;
            matrix1.m33 += matrix2.m33;
            return matrix1;
        }

        public static Matrix4x4 operator -(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            matrix1.m00 -= matrix2.m00;
            matrix1.m01 -= matrix2.m01;
            matrix1.m02 -= matrix2.m02;
            matrix1.m03 -= matrix2.m03;
            matrix1.m10 -= matrix2.m10;
            matrix1.m11 -= matrix2.m11;
            matrix1.m12 -= matrix2.m12;
            matrix1.m13 -= matrix2.m13;
            matrix1.m20 -= matrix2.m20;
            matrix1.m21 -= matrix2.m21;
            matrix1.m22 -= matrix2.m22;
            matrix1.m23 -= matrix2.m23;
            matrix1.m30 -= matrix2.m30;
            matrix1.m31 -= matrix2.m31;
            matrix1.m32 -= matrix2.m32;
            matrix1.m33 -= matrix2.m33;
            return matrix1;
        }

        #endregion Operators
    }
}
