Shader "Custom/URP_OutlineOnly"
{
    Properties
    {
        [HDR] _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.02
        
        [Toggle] _EnableGlow ("Enable Glow", Float) = 0
        _GlowIntensity ("Glow Multiplier", Float) = 2.0

        [Toggle] _EnablePulse ("Enable Pulse", Float) = 0
        _PulseSpeed ("Pulse Speed", Float) = 3.0
        _PulseMin ("Minimum Pulse Size", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" "Queue"="Geometry" }

        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "Outline" } 
            
            Cull Front 

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing 
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID 
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _OutlineColor;
                half _OutlineWidth;
                half _EnableGlow;
                half _GlowIntensity;
                half _EnablePulse;
                half _PulseSpeed;
                half _PulseMin;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                // Calculate pulse (0 to 1 range)
                half sineWave = (sin(_Time.y * _PulseSpeed) + 1.0) * 0.5;
                half pulseFactor = lerp(_PulseMin, 1.0, sineWave);
                
                // Only apply pulse if enabled
                half currentPulse = lerp(1.0, pulseFactor, _EnablePulse);

                // Extrude along normal
                float3 pushDirection = normalize(IN.normalOS);
                float3 expandedPos = IN.positionOS.xyz + (pushDirection * _OutlineWidth * currentPulse);

                OUT.positionHCS = TransformObjectToHClip(expandedPos);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);

                half4 finalColor = _OutlineColor;

                // Synchronize pulse with fragment color for a "breathing" emission
                half sineWave = (sin(_Time.y * _PulseSpeed) + 1.0) * 0.5;
                half pulseFactor = lerp(_PulseMin, 1.0, sineWave);
                half currentPulse = lerp(1.0, pulseFactor, _EnablePulse);

                // Apply glow multiplier (HDR push for Bloom)
                half currentGlow = lerp(1.0, _GlowIntensity, _EnableGlow);
                
                // Combine pulse and glow
                finalColor.rgb *= currentGlow * currentPulse;

                return finalColor;
            }
            ENDHLSL
        }
    }
}