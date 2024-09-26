#ifndef DISSOLVECOMMON_INCLUDED
#define DISSOLVECOMMON_INCLUDED


// Common defines
#define ArrayMax 4

#include "Assets/INab Studio/Common/Subgraphs/SDFs/3D_SDFs.hlsl"

// Calculate sdfs math on arrays of masks values
float CalculateSDFs(float3 worldSpacePosition, float4 Positions[ArrayMax], float4 Scales[ArrayMax],
	float4 Forwards[ArrayMax], float4 Rights[ArrayMax], float4 Ups[ArrayMax],
	float4 Positions_2[ArrayMax]) // float Angles[ArrayMax], float Lengths[ArrayMax], 
{

	int NumberOfMasks = 0;

#if defined(_MASKSNUMBER__1)
	NumberOfMasks = 1;
#elif _MASKSNUMBER__2
	NumberOfMasks = 2;
#elif _MASKSNUMBER__3 
	NumberOfMasks = 3;
#elif _MASKSNUMBER__4 
	NumberOfMasks = 4;
#endif

	float sum = 99999;

	[unroll]
	for (int i = 0; i < NumberOfMasks; i++)
	{
		float3 posDiff = Positions[i].xyz - worldSpacePosition;
		float3 posRotated = posDiff;

#if defined(_TYPE_SPHERE)
		sum = min(Sphere(posDiff, Scales[i].x), sum);
#elif _TYPE_BOX
		posRotated = PositionRotated(posDiff, Rights[i].xyz, Ups[i].xyz, Forwards[i].xyz);
		sum = min(Box(posRotated, Scales[i].xyz), sum);
#elif _TYPE_PLANE
		sum = min(Plane(Positions[i].xyz, worldSpacePosition, Ups[i].xyz), sum);
#elif _TYPE_ELLIPSE
		posRotated = PositionRotated(posDiff, Rights[i].xyz, Ups[i].xyz, Forwards[i].xyz);
		sum = min(Ellipsoid(posRotated, Scales[i].xyz), sum);
#elif _TYPE_SOLID_ANGLE
		posRotated = PositionRotated(posDiff, Rights[i].xyz, Ups[i].xyz, Forwards[i].xyz);
		float2 angle = float2(sin(Scales[i].x), cos(Scales[i].x));
		sum = min(SolidAngle(posRotated, angle, Scales[i].z), sum);
#elif _TYPE_ROUND_CONE
		sum = min(RoundCone(worldSpacePosition, Positions[i].xyz, Positions_2[i].xyz, Scales[i].x, Scales[i].y), sum);
#endif
	}

	return sum;

}

#endif