using System.Collections.Generic;
using UnityEngine;

    public class ItemStack
    {
        string filePath = string.Empty;
        List<GameObject> stack = null;
        GameObject owner;
        uint stackCount = 0; //
        
        public ItemStack(string filePath, uint count, GameObject owner)
        {
            this.filePath = filePath;
            this.owner = owner;
            this.stackCount = count;
            stack = GenerateStack(count);
        }

        List<GameObject> GenerateStack(uint count)
        {
            List<GameObject> res = new List<GameObject>();

            IncreaseStackCount(ref res, count);

            return res;
        }
        void IncreaseStackCount(ref List<GameObject> stack, uint count)
        {
            for(uint i = 0; i < count; i++)
            {
                var element = GameObject.Instantiate(Resources.Load<GameObject>(filePath), owner.transform);
                element.SetActive(false);
                element.transform.position = Vector3.zero;
                stack.Add(element);
            }
        }
        bool HasAvailableElement()
        {
            foreach(var element in stack)
            {
                if (!element.activeSelf) return true;
            }

            return false;
        }
        public GameObject GetElementFromStack()
        {
            if(HasAvailableElement())
            {
                foreach (var element in stack)
                {
                    if(!element.activeSelf)
                    {
                        UpdateStack();

                        return element;
                    }
                }
            }

            return null;
        }

        void UpdateStack()
        {
            if(stack[Mathf.Clamp(stack.Count - 2, 0, 500)].activeSelf)
            {
                IncreaseStackCount(ref stack, stackCount);
            }
        }
    }