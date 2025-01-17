﻿using System;
using System.Runtime.InteropServices;

namespace GXPEngine.Core
{
    public static class Soloud
    {
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_getVersion(IntPtr aSoloud);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Soloud_create();
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_init(IntPtr aSoloud);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_deinit(IntPtr aSoloud);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_stopAll(IntPtr aObjHandle);


        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_play(IntPtr aSoloud, IntPtr aSound);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_playEx(IntPtr aSoloud, IntPtr aSound, float aVolume, float aPan, bool aPaused, uint aBus);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_stop(IntPtr aSoloud, uint aVoiceHandle);

        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Wav_create();
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Wav_load(IntPtr aWav, string aFilename);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Wav_setLooping(IntPtr aWav, bool aLoop);

        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WavStream_create();
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint WavStream_load(IntPtr aWav, string aFilename);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint WavStream_setLooping(IntPtr aWav, bool aLoop);

        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Soloud_setSamplerate(IntPtr aSoloud, uint aVoiceHandle, float aSampleRate);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Soloud_getSamplerate(IntPtr aSoloud, uint aVoiceHandle);

        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Soloud_getPan(IntPtr aSoloud, uint aVoiceHandle);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Soloud_setPan(IntPtr aSoloud, uint aVoiceHandle, float aPan);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Soloud_getVolume(IntPtr aSoloud, uint aVoiceHandle);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Soloud_setVolume(IntPtr aSoloud, uint aVoiceHandle, float aVolume);

        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Soloud_getPause(IntPtr aSoloud, uint aVoiceHandle);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Soloud_setPause(IntPtr aSoloud, uint aVoiceHandle, bool aPause);
        [DllImport("../../GXPEngine/lib/soloud.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Soloud_isValidVoiceHandle(IntPtr aSoloud, uint aVoiceHandle);


    }
}
