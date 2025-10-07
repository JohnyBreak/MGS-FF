using UnityEngine;

namespace Sensors
{
    public class SightSensorMesh
    {
        private float _distance;
        private float _angle;
        private float _closeBottomHeight;
        private float _closeTopHeight;
        private float _farBottomHeight;
        private float _farTopHeight;

        public Mesh Mesh { get; private set; }
        public float Distance => _distance;
        
        public SightSensorMesh(
            float distance, 
            float angle, 
            float closeBottomHeight, 
            float closeTopHeight,
            float farBottomHeight,
            float farTopHeight)
        {
            _distance = distance;
            _angle = angle;
            _closeBottomHeight = closeBottomHeight;
            _closeTopHeight = closeTopHeight;
            _farBottomHeight = farBottomHeight;
            _farTopHeight = farTopHeight;
            Mesh = GetMesh();
        }

        public void UpdateMesh(
            float distance,
            float angle,
            float closeBottomHeight,
            float closeTopHeight,
            float farBottomHeight,
            float farTopHeight)
        {
            _distance = distance;
            _angle = angle;
            _closeBottomHeight = closeBottomHeight;
            _closeTopHeight = closeTopHeight;
            _farBottomHeight = farBottomHeight;
            _farTopHeight = farTopHeight;
            Mesh = GetMesh();
        }
        
        // private Mesh GetMesh()
        // {
        //     Mesh mesh = new Mesh();
        //     
        //     int segments = 10;
        //     int numTriangles = (segments * 4) + 4;
        //     int numVertices = numTriangles * 3;
        //
        //     Vector3[] vertices = new Vector3[numVertices];
        //     int[] triangles = new int[numVertices];
        //     
        //     Vector3 bottomCenter = Vector3.zero;
        //     Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        //     Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;
        //
        //     Vector3 topCenter = Vector3.zero + Vector3.up * _farTopHeight;
        //     Vector3 topLeft = bottomLeft + Vector3.up * _farTopHeight;
        //     Vector3 topRight = bottomRight + Vector3.up * _farTopHeight;
        //
        //     int vert = 0;
        //     
        //     // left side
        //     vertices[vert++] = bottomCenter;
        //     vertices[vert++] = bottomLeft;
        //     vertices[vert++] = topLeft;
        //     
        //     vertices[vert++] = topLeft;
        //     vertices[vert++] = topCenter;
        //     vertices[vert++] = bottomCenter;
        //     
        //     // right side
        //     vertices[vert++] = bottomCenter;
        //     vertices[vert++] = topCenter;
        //     vertices[vert++] = topRight;
        //     
        //     vertices[vert++] = topRight;
        //     vertices[vert++] = bottomRight;
        //     vertices[vert++] = bottomCenter;
        //
        //     float currentAngle = -_angle;
        //     float deltaAngle = (_angle * 2) / segments;
        //     for (int i = 0; i < segments; i++)
        //     {
        //         bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
        //         bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;
        //
        //         topLeft = bottomLeft + Vector3.up * _farTopHeight;
        //         topRight = bottomRight + Vector3.up * _farTopHeight;
        //         
        //         // far side
        //         vertices[vert++] = bottomLeft;
        //         vertices[vert++] = bottomRight;
        //         vertices[vert++] = topRight;
        //     
        //         vertices[vert++] = topRight;
        //         vertices[vert++] = topLeft;
        //         vertices[vert++] = bottomLeft;
        //     
        //         // top
        //         vertices[vert++] = topCenter;
        //         vertices[vert++] = topLeft;
        //         vertices[vert++] = topRight;
        //     
        //         // bottom
        //         vertices[vert++] = bottomCenter;
        //         vertices[vert++] = bottomRight;
        //         vertices[vert++] = bottomLeft;
        //         
        //         currentAngle += deltaAngle;
        //     }
        //     
        //     for (int i = 0; i < numVertices; i++)
        //     {
        //         triangles[i] = i;
        //     }
        //
        //     mesh.vertices = vertices;
        //     mesh.triangles = triangles;
        //     mesh.RecalculateNormals();
        //     
        //     return mesh;
        // }
        
        private Mesh GetMesh()
        {
            Mesh mesh = new Mesh();
        
            int segments = 10;
            int numTriangles = (segments * 4) + 4;
            int numVertices = numTriangles * 3;
        
            Vector3[] vertices = new Vector3[numVertices];
            int[] triangles = new int[numVertices];
            
            Vector3 bottomCenter = Vector3.zero - Vector3.up * _closeBottomHeight;
            Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * ((Vector3.forward * _distance) - Vector3.up * _farBottomHeight);
            Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * ((Vector3.forward * _distance) - Vector3.up * _farBottomHeight);
        
            Vector3 topCenter = Vector3.zero + Vector3.up * _closeTopHeight;
            Vector3 topLeft = bottomLeft + Vector3.up * _farBottomHeight + Vector3.up * _farTopHeight;
            Vector3 topRight = bottomRight + Vector3.up * _farBottomHeight + Vector3.up * _farTopHeight;
        
            int vert = 0;
            
            // left side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = topLeft;
            
            vertices[vert++] = topLeft;
            vertices[vert++] = topCenter;
            vertices[vert++] = bottomCenter;
            
            // right side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = topCenter;
            vertices[vert++] = topRight;
            
            vertices[vert++] = topRight;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomCenter;
        
            float currentAngle = -_angle;
            float deltaAngle = (_angle * 2) / segments;
            for (int i = 0; i < segments; i++)
            {
                bottomLeft = Quaternion.Euler(0, currentAngle, 0) * ((Vector3.forward * _distance) - Vector3.up * _farBottomHeight);
                bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * ((Vector3.forward * _distance) - Vector3.up * _farBottomHeight);
        
                topLeft = bottomLeft + Vector3.up * _farBottomHeight + Vector3.up * _farTopHeight;
                topRight = bottomRight + Vector3.up * _farBottomHeight + Vector3.up * _farTopHeight;
                
                // far side
                vertices[vert++] = bottomLeft;
                vertices[vert++] = bottomRight;
                vertices[vert++] = topRight;
            
                vertices[vert++] = topRight;
                vertices[vert++] = topLeft;
                vertices[vert++] = bottomLeft;
            
                // top
                vertices[vert++] = topCenter;
                vertices[vert++] = topLeft;
                vertices[vert++] = topRight;
            
                // bottom
                vertices[vert++] = bottomCenter;
                vertices[vert++] = bottomRight;
                vertices[vert++] = bottomLeft;
                
                currentAngle += deltaAngle;
            }
            
            for (int i = 0; i < numVertices; i++)
            {
                triangles[i] = i;
            }
        
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            
            return mesh;
        }
        
    }
}