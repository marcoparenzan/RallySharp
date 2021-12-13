﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Levels
{
    public static class Tilesheet
    {
        public const byte Width = 24;
        public const byte Height = 24;

        public static Rectangle[][] Rects;

        [ModuleInitializer]
        internal static void Initialize()
        {
            Rects = data.Select(xx => RectanglesOf(10, 4)).ToArray();
        }

        public static byte[] data0 = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52, 0x00, 0x00, 0x00, 0xF0, 0x00, 0x00, 0x00, 0x60, 0x08, 0x06, 0x00, 0x00, 0x00, 0x23, 0x33, 0xFF, 0xD1, 0x00, 0x00, 0x00, 0x01, 0x73, 0x52, 0x47, 0x42, 0x00, 0xAE, 0xCE, 0x1C, 0xE9, 0x00, 0x00, 0x00, 0x04, 0x67, 0x41, 0x4D, 0x41, 0x00, 0x00, 0xB1, 0x8F, 0x0B, 0xFC, 0x61, 0x05, 0x00, 0x00, 0x00, 0x09, 0x70, 0x48, 0x59, 0x73, 0x00, 0x00, 0x0E, 0xC3, 0x00, 0x00, 0x0E, 0xC3, 0x01, 0xC7, 0x6F, 0xA8, 0x64, 0x00, 0x00, 0x07, 0xFA, 0x49, 0x44, 0x41, 0x54, 0x78, 0x5E, 0xED, 0x9C, 0xE1, 0x6D, 0x5B, 0x49, 0x0C, 0x84, 0xD5, 0x64, 0x8A, 0xB9, 0x82, 0xAE, 0x91, 0x94, 0x72, 0x8D, 0x24, 0x78, 0xCE, 0x8E, 0x40, 0xAD, 0x87, 0xD2, 0x9E, 0x97, 0xC3, 0x67, 0xBD, 0x0C, 0x81, 0x0F, 0x96, 0x44, 0xED, 0x90, 0x22, 0x77, 0x20, 0xFB, 0x8F, 0x6F, 0xB7, 0x7F, 0x6E, 0xBF, 0x94, 0xFC, 0xF7, 0xEF, 0x0F, 0x29, 0x3F, 0x7F, 0xDC, 0x3E, 0xF8, 0xF1, 0xD3, 0x5C, 0x09, 0xEC, 0x75, 0xDE, 0xB3, 0xF9, 0x03, 0xE6, 0xB2, 0x64, 0xE0, 0x63, 0xA0, 0xF1, 0x31, 0x88, 0xEF, 0xC9, 0x40, 0x21, 0x15, 0xF8, 0x40, 0xB1, 0x2F, 0xF3, 0xFE, 0xC4, 0x8B, 0x8A, 0xC7, 0xE6, 0x91, 0x63, 0x36, 0xA9, 0x81, 0xD9, 0x50, 0x0F, 0xB2, 0xDC, 0x7C, 0x1E, 0x44, 0xB3, 0x29, 0xC0, 0x87, 0x61, 0x3D, 0x99, 0xF7, 0x25, 0x5E, 0x54, 0xEF, 0xF8, 0x91, 0x38, 0x93, 0x4F, 0x06, 0x3E, 0xDE, 0x80, 0x9F, 0x07, 0xBF, 0x7E, 0xFD, 0x21, 0x3E, 0xC7, 0x7B, 0x91, 0xC3, 0x73, 0x06, 0x33, 0x5D, 0x25, 0x5E, 0xEE, 0x35, 0x89, 0x97, 0xD4, 0xFB, 0xFD, 0x0C, 0xE6, 0xF2, 0x60, 0x60, 0x24, 0x61, 0xCA, 0x78, 0x20, 0xE6, 0x0E, 0xB2, 0xD7, 0x71, 0x16, 0x30, 0xD3, 0x55, 0xE2, 0x05, 0x5F, 0x13, 0xEC, 0xD5, 0xFB, 0xE5, 0x60, 0x2E, 0xD4, 0xC0, 0x11, 0x66, 0xD6, 0x57, 0x44, 0x4D, 0x66, 0xBA, 0x4A, 0xBC, 0xE0, 0x6B, 0x82, 0xBD, 0x7A, 0xBF, 0x1C, 0xCC, 0xE5, 0xD3, 0xAF, 0xD0, 0xD1, 0xB0, 0x99, 0x79, 0xF1, 0x4D, 0x8B, 0xE7, 0xC7, 0xB9, 0xF8, 0x38, 0xC2, 0x4C, 0x57, 0x89, 0x17, 0x7C, 0x4D, 0xB0, 0x57, 0xEF, 0x97, 0x83, 0xB9, 0xDC, 0x0D, 0xCC, 0xDE, 0xF4, 0x7F, 0x89, 0x5A, 0x78, 0xCC, 0x4C, 0x57, 0x89, 0x17, 0x7C, 0x4D, 0xB0, 0x57, 0xEF, 0x97, 0x83, 0xB9, 0x7C, 0x18, 0x78, 0x4E, 0xE2, 0xDB, 0x75, 0xFE, 0xA6, 0x8D, 0xF9, 0xF9, 0xF5, 0xF8, 0x1C, 0x8F, 0x6D, 0x60, 0xF3, 0x55, 0xB0, 0x57, 0xEF, 0x97, 0x83, 0xB9, 0x3C, 0x18, 0x18, 0xC6, 0x63, 0x66, 0x7C, 0xF6, 0x3C, 0x7B, 0x8F, 0x0D, 0x6C, 0xBE, 0x0A, 0xF6, 0xEA, 0xFD, 0x72, 0x30, 0x97, 0xFB, 0xAF, 0xD0, 0xCF, 0x8C, 0x8A, 0xF7, 0xC4, 0xFC, 0x41, 0x7C, 0xCF, 0xF1, 0x98, 0x9D, 0x61, 0xA6, 0xAB, 0xC4, 0x0B, 0xBE, 0x26, 0xD8, 0xAB, 0xF7, 0xCB, 0xC1, 0x5C, 0xEE, 0xDF, 0xC0, 0xCC, 0xA0, 0x33, 0x30, 0x25, 0xCB, 0x81, 0xF8, 0x9E, 0xE3, 0x27, 0x33, 0x5D, 0x25, 0x5E, 0xF0, 0x35, 0xC1, 0x5E, 0xBD, 0x5F, 0x0E, 0xE6, 0x72, 0x37, 0x30, 0xBE, 0x3D, 0x8F, 0x9F, 0xF3, 0x6B, 0x60, 0x7E, 0x1E, 0x5F, 0x8B, 0x39, 0x3C, 0xB6, 0x81, 0xCD, 0x57, 0xC1, 0x5E, 0xBD, 0x5F, 0x0E, 0xE6, 0xF2, 0xC9, 0xC0, 0x78, 0xCC, 0x8C, 0x1D, 0xF3, 0xF3, 0x73, 0xF6, 0xD8, 0x06, 0x36, 0x5F, 0x05, 0x7B, 0xF5, 0x7E, 0x39, 0x98, 0xCB, 0xFD, 0x6F, 0x60, 0xF6, 0xA6, 0x67, 0xB9, 0x83, 0x39, 0xC7, 0x9E, 0x33, 0xD3, 0x55, 0xE2, 0x05, 0x5F, 0x13, 0xEC, 0xD5, 0xFB, 0xE5, 0x60, 0x2E, 0xF7, 0x6F, 0xE0, 0x83, 0x67, 0xDF, 0xAC, 0xAB, 0xAF, 0x45, 0x3D, 0x1B, 0xD8, 0x7C, 0x15, 0xEC, 0xD5, 0xFB, 0xE5, 0x60, 0x2E, 0x9F, 0xBE, 0x81, 0x67, 0x43, 0xDF, 0x5F, 0xBF, 0xFD, 0x61, 0x36, 0xF0, 0xFC, 0x1C, 0x7A, 0x80, 0x99, 0xAE, 0x12, 0x2F, 0xF8, 0x9A, 0x60, 0xAF, 0xDE, 0x2F, 0x07, 0x73, 0x79, 0x30, 0x30, 0x7E, 0x56, 0x60, 0x03, 0x9B, 0x1D, 0xB0, 0x57, 0xEF, 0x97, 0x83, 0xB9, 0xDC, 0x0D, 0x1C, 0xCD, 0xFB, 0xEA, 0xD7, 0xE4, 0x19, 0xE4, 0xF0, 0x33, 0x6A, 0x32, 0xD3, 0x55, 0xE2, 0x05, 0x5F, 0x13, 0xEC, 0xD5, 0xFB, 0xE5, 0x60, 0x2E, 0xD4, 0xC0, 0x07, 0xB3, 0x61, 0x33, 0x03, 0xB3, 0xD7, 0xA3, 0x26, 0x33, 0x5D, 0x25, 0x5E, 0xF0, 0x35, 0xC1, 0x5E, 0xBD, 0x5F, 0x0E, 0xE6, 0xF2, 0x60, 0x60, 0x10, 0x4D, 0xF9, 0xF1, 0xB7, 0xEF, 0x78, 0x3D, 0x0A, 0xDC, 0xF3, 0xD3, 0xEB, 0x78, 0x2F, 0x60, 0xA6, 0xAB, 0xC4, 0x0B, 0xBE, 0x26, 0xD8, 0xAB, 0xF7, 0xCB, 0xC1, 0x5C, 0xA8, 0x81, 0x01, 0xDE, 0x7C, 0x18, 0xFA, 0x00, 0xAF, 0xE1, 0xF9, 0xF1, 0x78, 0x3E, 0x33, 0xC3, 0x4C, 0x57, 0x89, 0x97, 0x7C, 0x4D, 0xE2, 0x5E, 0xBD, 0xDB, 0xCF, 0x60, 0x2E, 0x4F, 0x0D, 0x0C, 0x70, 0x28, 0x3E, 0xC6, 0xF3, 0x57, 0x30, 0xD3, 0x55, 0x13, 0x17, 0xFD, 0xAE, 0xC4, 0xB9, 0x1A, 0xCF, 0xE7, 0x15, 0x98, 0xCB, 0x92, 0x81, 0x77, 0x60, 0x86, 0x53, 0x10, 0x97, 0xFD, 0x8E, 0xB0, 0x25, 0xFD, 0xCD, 0x78, 0x3E, 0xCF, 0xC1, 0x5C, 0x6E, 0xEA, 0x01, 0x5D, 0xC5, 0x60, 0xD5, 0x60, 0x2E, 0x6C, 0x66, 0x15, 0xCC, 0x75, 0x62, 0xED, 0x0A, 0x66, 0x5D, 0xD6, 0xC3, 0x0E, 0xB1, 0x96, 0x02, 0x75, 0xFF, 0x40, 0xA5, 0x0F, 0xDD, 0x16, 0x03, 0xA3, 0x86, 0x79, 0xE4, 0x98, 0x0D, 0x9B, 0x59, 0x05, 0xB1, 0x46, 0xAC, 0x59, 0x89, 0xD2, 0x00, 0xB1, 0x8E, 0x0A, 0x1B, 0x78, 0x01, 0xE8, 0x2B, 0x6B, 0xBC, 0x1B, 0x1D, 0x33, 0x89, 0x35, 0xAA, 0x6B, 0x29, 0xB5, 0x81, 0x4A, 0xF7, 0x40, 0xD9, 0xF7, 0x8C, 0xAA, 0x0E, 0x74, 0xDB, 0x0C, 0xCC, 0x72, 0x7F, 0x33, 0x5D, 0x73, 0x57, 0xD5, 0xE9, 0xD2, 0x67, 0xB9, 0x0A, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x36, 0xF0, 0x49, 0x74, 0xCD, 0x5D, 0x55, 0xA7, 0x4B, 0x9F, 0xE5, 0x2A, 0x50, 0xEB, 0x03, 0x55, 0x1D, 0xE8, 0xDA, 0xC0, 0x27, 0xD1, 0x35, 0x77, 0x55, 0x9D, 0x2E, 0x7D, 0x96, 0xAB, 0x40, 0xAD, 0x0F, 0x54, 0x75, 0xA0, 0x6B, 0x03, 0x9F, 0x44, 0xD7, 0xDC, 0x55, 0x75, 0xBA, 0xF4, 0x59, 0xAE, 0x02, 0xB5, 0x3E, 0x50, 0xD5, 0x81, 0xAE, 0x0D, 0x7C, 0x12, 0x5D, 0x73, 0x57, 0xD5, 0xE9, 0xD2, 0x67, 0xB9, 0x0A, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x36, 0xF0, 0x49, 0x74, 0xCD, 0x5D, 0x55, 0xA7, 0x4B, 0x9F, 0xE5, 0x2A, 0x50, 0xEB, 0x03, 0x55, 0x1D, 0xE8, 0xDA, 0xC0, 0x27, 0xD1, 0x35, 0x77, 0x55, 0x9D, 0x2E, 0x7D, 0x96, 0xAB, 0x40, 0xAD, 0x0F, 0x54, 0x75, 0xA0, 0x6B, 0x03, 0x9F, 0x44, 0xD7, 0xDC, 0x55, 0x75, 0xBA, 0xF4, 0x59, 0xAE, 0x02, 0xB5, 0x3E, 0x50, 0xD5, 0x81, 0xAE, 0x0D, 0x7C, 0x12, 0x5D, 0x73, 0x57, 0xD5, 0xE9, 0xD2, 0x67, 0xB9, 0x0A, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x36, 0xF0, 0x49, 0x74, 0xCD, 0x5D, 0x55, 0xA7, 0x4B, 0x9F, 0xE5, 0x2A, 0x50, 0xEB, 0x03, 0x55, 0x1D, 0xE8, 0xDA, 0xC0, 0x27, 0xD1, 0x35, 0x77, 0x55, 0x9D, 0x2E, 0x7D, 0x96, 0xAB, 0x40, 0xAD, 0x0F, 0x54, 0x75, 0xA0, 0x6B, 0x03, 0x9F, 0x44, 0xD7, 0xDC, 0x55, 0x75, 0xBA, 0xF4, 0x59, 0xAE, 0x02, 0xB5, 0x3E, 0x50, 0xD5, 0x81, 0xAE, 0x0D, 0x7C, 0x12, 0x5D, 0x73, 0x57, 0xD5, 0xE9, 0xD2, 0x67, 0xB9, 0x0A, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x36, 0xF0, 0x49, 0x74, 0xCD, 0x5D, 0x55, 0xA7, 0x4B, 0x9F, 0xE5, 0x2A, 0x50, 0xEB, 0x03, 0x55, 0x1D, 0xE8, 0xDA, 0xC0, 0x27, 0xD1, 0x35, 0x77, 0x55, 0x9D, 0x2E, 0x7D, 0x96, 0xAB, 0x40, 0xAD, 0x0F, 0x54, 0x75, 0xA0, 0x6B, 0x03, 0x9F, 0x44, 0xD7, 0xDC, 0x55, 0x75, 0xBA, 0xF4, 0x59, 0xAE, 0x02, 0xB5, 0x3E, 0x50, 0xD5, 0x81, 0xAE, 0x0D, 0x7C, 0x12, 0x5D, 0x73, 0x57, 0xD5, 0xE9, 0xD2, 0x67, 0xB9, 0x0A, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x36, 0xF0, 0x49, 0x74, 0xCD, 0x5D, 0x55, 0xA7, 0x4B, 0x9F, 0xE5, 0x2A, 0x50, 0xEB, 0x03, 0x55, 0x1D, 0xE8, 0xB6, 0x19, 0x58, 0x59, 0xE3, 0xDD, 0x50, 0xCF, 0x24, 0xEA, 0xAB, 0xEA, 0x28, 0x6B, 0x28, 0x34, 0x67, 0xD4, 0xFA, 0x40, 0x55, 0x07, 0xBA, 0x72, 0x03, 0xFB, 0x9F, 0xDA, 0xE5, 0xA8, 0x67, 0xD3, 0xF1, 0x4F, 0xE7, 0x94, 0x9F, 0xA1, 0xA3, 0x7F, 0x96, 0xAB, 0x00, 0xFA, 0xAA, 0x3A, 0xD0, 0x6D, 0x31, 0xB0, 0xFA, 0xA2, 0xBE, 0x23, 0xEA, 0xB9, 0xCC, 0xFA, 0x6C, 0x37, 0x3B, 0x64, 0x75, 0xAA, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x80, 0xBE, 0xAA, 0x0E, 0x74, 0x6F, 0xAA, 0x05, 0xCC, 0xB0, 0x26, 0x76, 0xB8, 0x8A, 0x3E, 0xCB, 0x55, 0x10, 0x7B, 0xBF, 0x02, 0xEC, 0x33, 0x7E, 0x67, 0xBA, 0xFA, 0xFE, 0x30, 0x70, 0x1C, 0x94, 0x0A, 0x56, 0x7C, 0x87, 0xAB, 0xE8, 0xB3, 0x5C, 0x05, 0xB1, 0xF7, 0x2B, 0xC0, 0x3E, 0xE3, 0x77, 0xA6, 0xAB, 0xEF, 0xFB, 0xAF, 0xD0, 0xEF, 0x36, 0x24, 0x75, 0xCF, 0x71, 0x2E, 0xCA, 0x3A, 0xC6, 0xEC, 0x20, 0xFF, 0x1B, 0x58, 0x85, 0xBA, 0x6F, 0xE8, 0xAB, 0xEB, 0x18, 0xB3, 0x83, 0x0D, 0x9C, 0x00, 0x7D, 0x75, 0x1D, 0x63, 0x76, 0xB0, 0x81, 0x13, 0xA0, 0xAF, 0xAE, 0x63, 0xCC, 0x0E, 0x36, 0x70, 0x02, 0xF4, 0xD5, 0x75, 0x8C, 0xD9, 0xC1, 0x06, 0x4E, 0x80, 0xBE, 0xBA, 0x8E, 0x31, 0x3B, 0xD8, 0xC0, 0x09, 0xD0, 0x57, 0xD7, 0x31, 0x66, 0x07, 0x1B, 0x38, 0x01, 0xFA, 0xEA, 0x3A, 0xC6, 0xEC, 0x60, 0x03, 0x27, 0x40, 0x5F, 0x5D, 0xC7, 0x98, 0x1D, 0x6C, 0xE0, 0x04, 0xE8, 0xAB, 0xEB, 0x18, 0xB3, 0x83, 0x0D, 0x9C, 0x00, 0x7D, 0x75, 0x1D, 0x63, 0x76, 0xB0, 0x81, 0x13, 0xA0, 0xAF, 0xAE, 0x63, 0xCC, 0x0E, 0x36, 0x70, 0x02, 0xF4, 0xD5, 0x75, 0x8C, 0xD9, 0xC1, 0x06, 0x4E, 0x80, 0xBE, 0xBA, 0x8E, 0x31, 0x3B, 0xD8, 0xC0, 0x09, 0xD0, 0x57, 0xD7, 0x31, 0x66, 0x07, 0x1B, 0x38, 0x01, 0xFA, 0xEA, 0x3A, 0xC6, 0xEC, 0x60, 0x03, 0x27, 0x40, 0x5F, 0x5D, 0xC7, 0x98, 0x1D, 0x6C, 0xE0, 0x04, 0xE8, 0xAB, 0xEB, 0x18, 0xB3, 0x83, 0x0D, 0x9C, 0x00, 0x7D, 0x75, 0x1D, 0x63, 0x76, 0xB0, 0x81, 0x09, 0xD0, 0x8E, 0xB0, 0xF7, 0x19, 0x73, 0x36, 0x32, 0x03, 0xAB, 0x2F, 0x3E, 0xF4, 0x95, 0x28, 0xFF, 0xAD, 0x29, 0x88, 0xF5, 0x94, 0xB0, 0xDA, 0xDF, 0x19, 0xF6, 0x19, 0x14, 0xB0, 0xDA, 0xEF, 0x00, 0xFA, 0xB7, 0x81, 0x13, 0xE6, 0x7F, 0xB7, 0xCB, 0x7A, 0xA8, 0x20, 0xD6, 0x54, 0xC2, 0x6A, 0x7F, 0x67, 0xD8, 0x67, 0x50, 0xC0, 0x6A, 0xBF, 0x03, 0xE8, 0x5F, 0xF6, 0x7F, 0xA1, 0xD5, 0x06, 0x50, 0xE9, 0x82, 0x2E, 0x7D, 0xCC, 0x29, 0x83, 0x9D, 0x5D, 0x01, 0xFA, 0xAF, 0x60, 0x67, 0x57, 0xA8, 0xD2, 0x79, 0xC5, 0xCD, 0xF1, 0x3C, 0x8E, 0x4B, 0x32, 0x2F, 0xA3, 0x8A, 0xA8, 0xCD, 0x96, 0xB3, 0x83, 0x4A, 0x17, 0x74, 0xE9, 0x47, 0xB3, 0x32, 0xD8, 0xD9, 0x15, 0xA0, 0xFF, 0x0A, 0x76, 0x76, 0x85, 0x2A, 0x9D, 0x57, 0x8C, 0x6B, 0xEA, 0xC8, 0x42, 0xB1, 0x04, 0x85, 0x66, 0x37, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x85, 0x6A, 0x11, 0x5D, 0x0B, 0x56, 0xA1, 0xEE, 0x1F, 0xFA, 0xCC, 0xB4, 0x11, 0x76, 0x76, 0x85, 0xAE, 0xFE, 0x59, 0xAE, 0x92, 0x71, 0x4D, 0x1D, 0x59, 0xA8, 0x16, 0xD1, 0xB5, 0x60, 0x15, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0x05, 0x16, 0x51, 0xB9, 0x0C, 0x85, 0x66, 0x37, 0xEA, 0xFE, 0xA1, 0xCF, 0x4C, 0x1B, 0x61, 0x67, 0x57, 0xE8, 0xEA, 0x9F, 0xE5, 0x2A, 0x19, 0xD7, 0xD4, 0x91, 0xC5, 0x71, 0x49, 0xB0, 0x8C, 0x6A, 0x76, 0x2E, 0xE0, 0x2A, 0xA8, 0xC5, 0x72, 0x3B, 0xC4, 0xCF, 0xF1, 0x0C, 0x76, 0x76, 0x05, 0x9C, 0x8F, 0x66, 0x65, 0xB0, 0xB3, 0x2B, 0xEC, 0xF6, 0xF7, 0x0A, 0xB5, 0x3E, 0x18, 0xD7, 0xD4, 0x91, 0x05, 0x2E, 0x0A, 0x16, 0x52, 0xC5, 0xEE, 0x05, 0x5C, 0x05, 0xF5, 0x58, 0x6E, 0x87, 0xF8, 0x59, 0x9E, 0xC1, 0xCE, 0xAE, 0x80, 0xF3, 0x98, 0x53, 0x06, 0x3B, 0xBB, 0xC2, 0x6E, 0x7F, 0xAF, 0x50, 0xEB, 0x83, 0x71, 0x4D, 0x1D, 0x34, 0x6E, 0xB7, 0xDF, 0x0A, 0xDE, 0xDC, 0x6A, 0xBF, 0x4C, 0x61, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };

        static byte[][] data = { data0 };

        static Rectangle[] RectanglesOf(int width, int height)
        {
            var xx = new Rectangle[height * width];
            var z = 0;
            for (byte j = 0; j < height; j++)
                for (byte i = 0; i < width; i++)
                    xx[z++] = new Rectangle(i * Width, j * Height, Width, Height);
            return xx;
        }
    }
}
