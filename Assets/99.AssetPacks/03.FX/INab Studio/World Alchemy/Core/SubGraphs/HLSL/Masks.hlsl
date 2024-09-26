#ifndef SPHEREMASK_INCLUDED
#define SPHEREMASK_INCLUDED

#include "Common.hlsl"
#include "Assets/INab Studio/Common/Subgraphs/SDFs/3D_SDFs.hlsl"


// Common
float4 Positions[ArrayMax];
float4 Scales[ArrayMax];

// Rotation
float4 Forwards[ArrayMax];
float4 Rights[ArrayMax];
float4 Ups[ArrayMax];

// Round Cones
float4 Positions_2[ArrayMax];


void Masks_float(float3 worldSpacePosition, out float Out)
{
	float sum = CalculateSDFs(worldSpacePosition, Positions, Scales,
		Forwards, Rights, Ups,
		Positions_2); // Angles, Lengths, 

	Out = sum;

}

#endif