﻿using System.Collections.Generic;

namespace uTinyRipper.Exporter.YAML
{
	public sealed class YAMLSequenceNode : YAMLNode
	{
		public YAMLSequenceNode()
		{
		}

		public YAMLSequenceNode(SequenceStyle style)
		{
			Style = style;
		}

		public void Add(bool value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(byte value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(short value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(ushort value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(int value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(uint value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(long value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(ulong value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(float value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(double value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			node.Style = Style.ToScalarStyle();
			Add(node);
		}

		public void Add(string value)
		{
			YAMLScalarNode node = new YAMLScalarNode(value);
			Add(node);
		}

		public void Add(YAMLNode child)
		{
			m_children.Add(child);
		}
		
		internal override void Emit(Emitter emitter)
		{
			base.Emit(emitter);

			StartChildren(emitter);
			foreach (YAMLNode child in m_children)
			{
				StartChild(emitter, child);
				child.Emit(emitter);
				EndChild(emitter, child);
			}
			EndChildren(emitter);
		}

		private void StartChildren(Emitter emitter)
		{
			switch (Style)
			{
				case SequenceStyle.Block:
					if (m_children.Count == 0)
					{
						emitter.Write('[');
					}
					break;

				case SequenceStyle.BlockCurve:
					if (m_children.Count == 0)
					{
						emitter.Write('{');
					}
					break;

				case SequenceStyle.Flow:
					emitter.Write('[');
					break;

				case SequenceStyle.Raw:
					if (m_children.Count == 0)
					{
						emitter.Write('[');
					}
					break;
			}
		}

		private void EndChildren(Emitter emitter)
		{
			switch (Style)
			{
				case SequenceStyle.Block:
					if (m_children.Count == 0)
					{
						emitter.Write(']');
					}
					emitter.WriteLine();
					break;

				case SequenceStyle.BlockCurve:
					if (m_children.Count == 0)
					{
						emitter.WriteClose('}');
					}
					emitter.WriteLine();
					break;

				case SequenceStyle.Flow:
					emitter.WriteClose(']');
					break;

				case SequenceStyle.Raw:
					if (m_children.Count == 0)
					{
						emitter.Write(']');
					}
					emitter.WriteLine();
					break;
			}
		}

		private void StartChild(Emitter emitter, YAMLNode next)
		{
			if(Style == SequenceStyle.Block || Style == SequenceStyle.BlockCurve)
			{
				emitter.Write('-').WriteWhitespace();

				if(next.NodeType == NodeType)
				{
					emitter.IncreaseIntent();
				}
			}
			if(next.IsIndent)
			{
				emitter.IncreaseIntent();
			}
		}

		private void EndChild(Emitter emitter, YAMLNode next)
		{
			if(Style.IsAnyBlock())
			{
				emitter.WriteLine();
				if(next.NodeType == NodeType)
				{
					emitter.DecreaseIntent();
				}
			}
			else if(Style == SequenceStyle.Flow)
			{
				emitter.WriteSeparator().WriteWhitespace();
			}
			if (next.IsIndent)
			{
				emitter.DecreaseIntent();
			}
		}

		public static YAMLSequenceNode Empty { get; } = new YAMLSequenceNode();

		public override YAMLNodeType NodeType => YAMLNodeType.Sequence;
		public override bool IsMultyline
		{
			get
			{
				return Style.IsAnyBlock() && m_children.Count > 0;
			}
		}
		public override bool IsIndent => false;

		public SequenceStyle Style { get; set; }

		private readonly List<YAMLNode> m_children = new List<YAMLNode>();
	}
}
