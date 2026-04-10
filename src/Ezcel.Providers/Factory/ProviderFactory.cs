using Ezcel.Providers.Abstractions;
using Ezcel.Providers.BuiltIn;
using Ezcel.Providers.Custom;
using System.Collections.Generic;
using System.Linq;

namespace Ezcel.Providers.Factory
{
    public class ProviderFactory
    {
        private readonly List<IProviderFactory> _factories;

        public ProviderFactory()
        {
            _factories = new List<IProviderFactory>
            {
                new OpenAIProvider(),
                new CustomProvider()
                // 后续可以添加其他内置提供者
            };
        }

        public IChatClient CreateClient(ProviderConfig config)
        {
            var factory = _factories.FirstOrDefault(f => f.Supports(config.Type));
            if (factory == null)
            {
                throw new System.ArgumentException($"不支持的提供者类型: {config.Type}");
            }

            return factory.CreateClient(config);
        }

        public List<ProviderMetadata> GetAllProviders()
        {
            var metadataList = new List<ProviderMetadata>();
            foreach (var factory in _factories)
            {
                metadataList.Add(factory.GetMetadata());
            }
            return metadataList;
        }

        public ProviderMetadata GetProviderMetadata(ProviderType type)
        {
            var factory = _factories.FirstOrDefault(f => f.Supports(type));
            if (factory == null)
            {
                throw new System.ArgumentException($"不支持的提供者类型: {type}");
            }
            return factory.GetMetadata();
        }
    }
}