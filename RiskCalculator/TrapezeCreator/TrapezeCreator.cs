using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskCalculator.Models;

namespace RiskCalculator
{
    static class TrapezeCreator
    {
        static public List<TrapezeModel> CreateTrapezeList(double lengthAsixX, int countTrap, params double [] intervalArr)
        {            
            // Если массив с интервалами не передавался, значит нужно построить равномерные трапеции.
            if(intervalArr.Length == 0)
            {
                return CreateConstTrapezeList(lengthAsixX, countTrap);
            }

            // Список интервалов определяющих размер трапеции.
            List<IntervalModel> intervalList = new List<IntervalModel>();

            for (int i = 0; i < intervalArr.Length; i++)
            {
                intervalList.Add(new IntervalModel() { a = intervalArr[i], b = intervalArr[++i]});
            }

            // Этап 1 – Определение корректирующих параметров.

            // Массив корректирующих параметров.
            double[] h = new double[countTrap];

            // Нахождение корректирующих параметров.
            for (int i = 0; i < countTrap; i++)
            {
                h[i] = (intervalList[i].b - intervalList[i].a) / (intervalList.Count - 1);
            }

            // Этап 2 - Вычисление абсиц нечетких чисел.

            // Список трапеций.
            List<TrapezeModel> trapList = new List<TrapezeModel>();

            for (int i = 0; i < countTrap; i++)
            {
                trapList.Add(new TrapezeModel()
                {
                    a = intervalList[i].a - h[i],
                    c = intervalList[i].b + h[i],
                    b11 = intervalList[i].a + h[i],
                    b21 = intervalList[i].b - h[i]
                });
            }

            // Этап 3 – Определение базового значения сдвига и поправка термов.

            // Определение сдвига. //Округлить до 2 знаков.
            double sf = trapList[0].b11 - intervalList[0].a;

            // Список поправленных термов.
            List<TrapezeModel> corrTrapList = new List<TrapezeModel>();

            // Поправка термов.
            for (int i = 0; i < countTrap; i++)
            {
                corrTrapList.Add(new TrapezeModel()
                {
                    a = trapList[i].a - sf,
                    b11 = trapList[i].b11 - sf,
                    b21 = trapList[i].b21 - sf,
                    c = trapList[i].c - sf
                });
            }

            // Этап 4 – Нормирование результирующих НЧ.

            List<TrapezeModel> resTrapList = new List<TrapezeModel>();

            double n = lengthAsixX;

            for (int i = 0; i < countTrap; i++)
            {
                double a = (corrTrapList[i].a * n) / corrTrapList[countTrap - 1].b21;
                double b11 = (corrTrapList[i].b11 * n) / corrTrapList[countTrap - 1].b21;
                double b21 = (corrTrapList[i].b21 * n) / corrTrapList[countTrap - 1].b21;
                double c = (corrTrapList[i].c * n) / corrTrapList[countTrap - 1].b21;

                resTrapList.Add(new TrapezeModel()
                {
                    a = a < 0 ? 0 : a > n ? n : a,
                    b11 = b11 < 0 ? 0 : b11 > n ? n : b11,
                    b21 = b21 < 0 ? 0 : b21 > n ? n : b21,
                    c = c < 0 ? 0 : c > n ? n : c
                });
            }

            return resTrapList;
        }
        static private List<TrapezeModel> CreateConstTrapezeList(double lengthAsixX, int countTrap)
        {
            // Список трапеций.
            List<TrapezeModel> trapList = new List<TrapezeModel>();

            // Определение сдвига для равномерного распределения.
            double shift = (lengthAsixX / (countTrap + countTrap - 1));

            // Создание первой трапеции.
            trapList.Add(new TrapezeModel()
            {
                a = 0,
                b11 = 0,
                b21 = shift,
                c = shift * 2
            });

            // Цикл создания оставшихся трапеций.
            // Вычисление их абсцис.
            for (int i = 0; i < countTrap - 1; i++)
            {
                double a = trapList[i].b21;
                double b11 = a + shift;
                double b21 = b11 + shift;
                double c = (b21 + shift) > lengthAsixX ? lengthAsixX : (b21 + shift);

                trapList.Add(new TrapezeModel()
                {
                    a = a,
                    b11 = b11,
                    b21 = b21,
                    c = c
                });
            }

            return trapList;
        }
        static private bool IsUniformInterval(List<TrapezeModel> trapList)
        {
            // Является ли интервал равномерным.
            bool isUniformInterval = true;

            // Цикл проверки условия равномерности.
            for (int i = 0; i < trapList.Count; i++)
            {
                if (i + 1 == trapList.Count)
                    break;
                double d1 = Math.Round(trapList[i].b21 - trapList[i].b11, 4);
                double d2 = Math.Round(trapList[i + 1].b21 - trapList[i + 1].b11, 4);
                // Если интервал неравномерный, выходим из цикла.
                if ( d1 != d2)
                {
                    isUniformInterval = false;
                    break;
                }
            }

            return isUniformInterval;
        }
        static public List<TrapezeModel> IncrementTrapezeList(List<TrapezeModel> trapezes, double lengthAsixX)
        {
            // Если интервал равномерный.
            if(IsUniformInterval(trapezes))
            {
                return CreateConstTrapezeList(lengthAsixX, trapezes.Count + 1);
            }

            // Иначе - интервал неравномерный, производим инкрементирование трапеций (термов).

            int trapCount = trapezes.Count;

            double sum = 0;

            for (int i = 0; i < trapCount; i++)
            {
                sum += trapezes[i].b21 - trapezes[i].b11;
            }

            double k1 = sum / trapezes.Count;

            sum = 0;

            for (int i = 1; i < trapCount; i++)
            {
                sum += trapezes[i].b11 - trapezes[i - 1].b21;
            }

            double k2 = sum / (trapCount - 1);

            double k = k1 + k2;

            sum = 0;

            for (int i = 2; i < trapCount; i++)
            {
                sum += trapezes[i].a - trapezes[i - 2].c;
            }

            sum += trapezes[1].a - trapezes[0].a + trapezes[trapCount - 1].c - trapezes[trapCount - 2].c;

            double l1 = sum / trapCount;

            sum = 0;

            for (int i = 1; i < trapCount; i++)
            {
                sum += trapezes[i - 1].c - trapezes[i].a;
            }

            double l2 = sum / (trapCount - 1);

            double l = l1 + l2;

            // Second step.

            double[] x = new double[trapCount];

            for (int i = 0; i < trapCount; i++)
            {
                x[i] = trapezes[i].b21 - trapezes[i].b11;
            }

            // Определяем позицию куда добавится трапеция.

            int s = 0;

            for (int i = 0; i < trapCount; i++)
            {
                if (i == trapCount - 1)
                    break;
                if ((k1 >= x[i] && k <= x[i + 1]) || (k1 <= x[i] && k >= x[i + 1]))
                {
                    s = i;
                }
            }

            List<TrapezeModel> resTrapList = new List<TrapezeModel>();

            for (int i = 0; i < trapCount + 1; i++)
            {
                double b11 = i < s + 1 ? trapezes[i].b11 : i == s + 1 ? trapezes[i - 1].b21 + k2 : i > s + 1 ? trapezes[i - 1].b11 + k : 0;
                double a = i < s + 2 ? trapezes[i].a : i == s + 2 ? trapezes[i - 2].c + l1 : i > s + 2 ? trapezes[i - 1].a + l : 0;
                double b21 = i < s + 1 ? trapezes[i].b21 : i == s + 1 ? trapezes[i].b11 + k1 : i > s + 1 ? trapezes[i - 1].b21 + k : 0;
                double c = i < s ? trapezes[i].c : i == s ? trapezes[i + 1].a + l2 : i > s ? trapezes[i - 1].c + l : 0;

                resTrapList.Add(new TrapezeModel()
                {
                    b11 = b11,
                    a = a,
                    b21 = b21,
                    c = c
                });
            }

            // Fourth step.

            double k3 = trapezes[trapCount - 1].b21 / resTrapList[trapCount].b21;

            for (int i = 0; i < resTrapList.Count; i++)
            {
                double b11 = resTrapList[i].b11 * k3;
                double a = resTrapList[i].a * k3;
                double b21 = resTrapList[i].b21 * k3;
                double c = (resTrapList[i].c * k3) > 100 ? 100 : (resTrapList[i].c * k3);

                resTrapList[i] = new TrapezeModel { b11 = b11, a = a, b21 = b21, c = c };
            }

            return resTrapList;
        }
        static public List<TrapezeModel> DecrementTrapezeList(List<TrapezeModel> trapList, double lengthAsixX)
        {
            if(IsUniformInterval(trapList))
            {
                return CreateConstTrapezeList(lengthAsixX, trapList.Count - 1);
            }

            return trapList;
        }
    }
}
