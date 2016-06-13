﻿using System;
using Random = UnityEngine.Random;

namespace ProceduralToolkit.Examples
{
    public class CellularAutomaton
    {
        public CellState[,] cells;

        private int width;
        private int height;
        private Ruleset ruleset;
        private bool aliveBorders;

        private CellState[,] copy;

        public CellularAutomaton(int width, int height, Ruleset ruleset, float startNoise, bool aliveBorders)
        {
            this.width = width;
            this.height = height;
            this.ruleset = ruleset;
            this.aliveBorders = aliveBorders;
            cells = new CellState[width, height];
            copy = new CellState[width, height];

            FillWithNoise(startNoise);
        }

        public void FillWithNoise(float noise)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = Random.value < noise ? CellState.Alive : CellState.Dead;
                }
            }
        }

        public void Simulate(int generations)
        {
            for (int i = 0; i < generations; i++)
            {
                Simulate();
            }
        }

        public void Simulate()
        {
            PTUtils.Swap(ref cells, ref copy);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int aliveCells = CountAliveNeighbourCells(copy, x, y);

                    if (copy[x, y] == CellState.Dead)
                    {
                        if (ruleset.CanSpawn(aliveCells))
                        {
                            cells[x, y] = CellState.Alive;
                        }
                        else
                        {
                            cells[x, y] = CellState.Dead;
                        }
                    }
                    else
                    {
                        if (!ruleset.CanSurvive(aliveCells))
                        {
                            cells[x, y] = CellState.Dead;
                        }
                        else
                        {
                            cells[x, y] = CellState.Alive;
                        }
                    }
                }
            }
        }

        private int CountAliveNeighbourCells(CellState[,] grid, int x, int y)
        {
            int count = 0;
            if (aliveBorders)
            {
                grid.VisitMooreNeighbours(x, y, false, (neighbourX, neighbourY) =>
                {
                    if (grid.IsInBounds(neighbourX, neighbourY))
                    {
                        if (grid[neighbourX, neighbourY] == CellState.Alive)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        count++;
                    }
                });
            }
            else
            {
                grid.VisitMooreNeighbours(x, y, true, (neighbourX, neighbourY) =>
                {
                    if (grid[neighbourX, neighbourY] == CellState.Alive)
                    {
                        count++;
                    }
                });
            }
            return count;
        }
    }
}